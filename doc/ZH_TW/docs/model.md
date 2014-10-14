# model doc

##ColumnTypeAttribute

用來宣告Property對應的資料庫型別,長度，是否可為null和評論.此attribute只能用在class的Property.以下為Property為CUST_SEQ對應資料庫的型別為String,長度為20,不可為null和評論.


    [ColumnType(DbType.String, "20", false, "This is a comment."]
    public string CUST_SEQ { get; set;}

 
##ColumnNameAttribute

用來宣告Property對應的資料庫欄位名字,不必與Property相同.以下範例為MyProperty對應資料庫的MyDbColumnName的欄位.


    [ColumnName("MyDbColumnName")]
    public string MyProperty { get; set;}
 
##TableNameAttribute

用來宣告class對應的資料庫表格名稱,此attribute只能用在對class宣告.

    [TableName("MyTableName")]
    public class MyClassName
    {
    }
 
##PrimaryKeyAttribute

用來宣告Property對應資料庫中欄位的主要鍵.宣告為PrimaryKeyAttribute的Property在建立資料表時會宣告為主要鍵.

    [PrimaryKey]
    public string ID { get; set;}
 
##NotGenColumnAttribute

用來宣告Property中不產生對應資料庫欄位，被宣告為NotGenColumn的Property在建立資料表時不會產生對應的欄位。

    [NotGenColumn]
    public string NotInDB { get; set;}

 
##IndexAttribute

用來宣告Property對應資料庫中欄位的索引鍵.宣告為IndexAttribute的Property在建立資料表時會宣告為索引鍵.

    [Index]
    public string ID { get; set;}
