
using CloudModel.DataModel.Raw;

public static class DataCenter
{
    /// <summary>
    /// only allow these keys for admin.
    /// </summary>
    private static string[] KEYS = new []
    {
        "UnitDataTable",
        "PetDataTable",
        "EquipmentDataTable",
        "ItemDataTable",
        "SkillDataTable",
        "EffectDataTable",
        "QuestDataTable"
    };

    private static Dictionary<string, RowDataTable> _dataTables = new Dictionary<string, RowDataTable>();

    public static bool TryAddDataTable(string key, string json)
    {
        if (string.IsNullOrEmpty(key)) return false;
        if(Array.IndexOf(KEYS, key) == -1) return false;

        if (_dataTables.ContainsKey(key))
        {
            _dataTables[key] = new RowDataTable(key, json);
            return true;
        }
        _dataTables.Add(key, new RowDataTable(key, json));
        return true;
    }
    
    public static bool TryGetData(string key, string name, out RowData rowData)
    {
        if (!_dataTables.TryGetValue(key, out RowDataTable dataTable) ||
            string.IsNullOrEmpty(key) ||
            Array.IndexOf(KEYS, key) == -1)
        {
            rowData = null;
            return false;
        }

        return dataTable.TryGetData(name, out rowData);
    }
    public static bool TryGetDataList(string key, string[] names, out List<RowData> rowDataList)
    {
        if (!_dataTables.TryGetValue(key, out RowDataTable dataTable) ||
            string.IsNullOrEmpty(key) ||
            Array.IndexOf(KEYS, key) == -1)
        {
            rowDataList = null;
            return false;
        }

        return dataTable.TryGetDataList(names, out rowDataList);
    }
}
