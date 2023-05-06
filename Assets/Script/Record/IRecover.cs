

public interface IRecover
{
    /// <summary>
    /// 读取
    /// </summary>
    /// <param name="messageAssetName"></param>
    /// <param name="currentIndex"></param>
    void RecoverData(string messageAssetName, int currentIndex);
}

public interface IRecord
{
    void StartRecord(string messageAssetName, int currentIndex);
    /// <summary>
    /// 记录所需保存的数据
    /// </summary>
    /// <param name="messageAssetName=保存数据的资源名称"></param>
    /// <param name="targetName=保存绑定对象的名称"></param>
    /// <param name="currentIndex=保存对象的段落"></param>
    /// <param name="value=保存的值"></param>
    void RecordData(string messageAssetName, string targetName, int currentIndex, object value);
}
