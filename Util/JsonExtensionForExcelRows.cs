using CloudModel.DataModel.Raw;
namespace CloudModel.Util;

/// <summary>
/// This might will be deleted when next release contains the update Cloud Storage SDK or Cloud Save SDK.
/// My Idea is not use Cloud Save , but already parsed Game Data for security. inefficient method.
/// Create json string value on the Unity Editor. Here is the Save Center Converted Json string from Excel Rows.
/// </summary>
public static class JsonExtensionForExcelRows
{
    public static string ToJson(RowData rowData)
    {
        return ToJsonFormat(rowData);
    }
    public static string ToJson(List<RowData> rowDatas)
    {
        return ToJsonFormat(rowDatas);
    }

    public static List<RowData> FromJson(string json)
    {
        List<RowData> rowDataList = new List<RowData>();
        string[] rows = json.Split('\n');
        for (int i = 0; i < rows.Length; i++)
        {
            string row = rows[i];
            // trim signatures
            if (i == 0)
            {
                row = row.Replace("[", "");
                row = row.Replace("\n", "");
                row = row.Replace("]", "");
            }
            else if (i == rows.Length - 1)
            {
                row = row.Replace("\n", "");
            }
            else
            {
                row = row.Replace("\n", "");
                row = row.Replace("]", "");
            }
            string[] columns = row.Split(',');
            RowData rowData = new RowData();
            rowData.ColumnHeaders = new List<string>();
            rowData.ColumnValues = new List<string>();
            for (int j = 0; j < columns.Length; j++)
            {
                // process delete { } and \n \r
                string column = columns[j];
                if (j == 0)
                {
                    column = column.Replace("{", "");
                    column = column.Replace("\n", "");
                }
                else if (j == columns.Length - 1)
                {
                    column = column.Replace("}", "");
                    column = column.Replace("\n", "");
                }
                else
                {
                    column = column.Replace("\n", "");
                }

                // divide key and value
                string[] keyValue = column.Split(':');

                string key = keyValue[0];
                if (string.IsNullOrEmpty(key)) continue;
                string value = keyValue.Length <= 1 ? string.Empty : keyValue[1];
                if (j == 0)
                {
                    rowData.FirstColumnValue = value;
                }
                rowData.ColumnHeaders.Add(key);
                rowData.ColumnValues.Add(value);
            }
            if(string.IsNullOrEmpty(rowData.FirstColumnValue)) continue;
            rowDataList.Add(rowData);
        }

        return rowDataList;
    }
    private static string ToJsonFormat(RowData rowData)
    {
        string rowJson = "";
        for (int j = 0; j < rowData.ColumnHeaders.Count; j++)
        {
            if (j == 0)
                rowJson += $"\t{{\n\t\t{rowData.ColumnHeaders[j]} : {rowData.ColumnValues[j]},\n";
            else if(j != rowData.ColumnHeaders.Count - 1)
                rowJson += $"\t\t{rowData.ColumnHeaders[j]} : {rowData.ColumnValues[j]},\n";
            else
                rowJson += $"\t\t{rowData.ColumnHeaders[j]} : {rowData.ColumnValues[j]}\n" + "\t}";
        }
        return rowJson;
    }
    private static string ToJsonFormat(List<RowData> rowDataList)
    {
        string result = "";
        for (int i = 0; i < rowDataList.Count; i++)
        {
            RowData rowData = rowDataList[i];
            string rowJson = "";
            for (int j = 0; j < rowData.ColumnHeaders.Count; j++)
            {
                if (j == 0)
                    rowJson += $"\t{{\n\t\t{rowData.ColumnHeaders[j]} : {rowData.ColumnValues[j]},\n";
                else if(j != rowData.ColumnHeaders.Count - 1)
                    rowJson += $"\t\t{rowData.ColumnHeaders[j]} : {rowData.ColumnValues[j]},\n";
                else
                    rowJson += $"\t\t{rowData.ColumnHeaders[j]} : {rowData.ColumnValues[j]}\n" + "\t}";
            }

            // if not last row
            if(i == 0)
            {
                rowJson = rowDataList.Count == 1
                    ? $@"[
{rowJson}
]"
                    : $@"[
{rowJson}," + '\n';
            }
            else if ( i != rowDataList.Count - 1)
                rowJson = $"{rowJson},\n";
            else
                rowJson = $@"{rowJson}
]";
            result += rowJson;
        }

        return result;
    }
}