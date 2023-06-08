using CloudModel.DataModel.Raw;
using CloudModel.Util;

internal class RowDataTable
{
    private string _key;
    private Dictionary<string, RowData> _dataList;

    public RowDataTable(string key, string json)
    {
        _key = key;
        List<RowData> list = JsonExtensionForExcelRows.FromJson(json);
        _dataList = new Dictionary<string, RowData>(list.Count);
        
        foreach (RowData rowData in list)
        {
            _dataList.Add(rowData.FirstColumnValue, rowData);
        }
    }
    
    public bool TryGetData(string name, out RowData rowData)
    {
        return _dataList.TryGetValue(name, out rowData);
    }
    
    public bool TryGetDataList(string[] names, out List<RowData> rowDatas)
    {
        List<RowData> rowDataList = new List<RowData>();
        for (var index = 0; index < names.Length; index++)
        {
            if (!_dataList.TryGetValue(names[index], out RowData rowData)) continue;
            rowDataList.Add(rowData);
        }

        rowDatas = rowDataList;
        return rowDatas.Count > 0;
    }
}
