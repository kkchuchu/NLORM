# model doc

##ColumnTypeAttribute

用來宣告Property對應的資料庫型別,長度，是否可為null和評論.此attribute只能用在class的Property.以下為Property為CUST_SEQ對應資料庫的型別為String,長度為20,不可為null和評論.

''''
[ColumnType(DbType.String, "20", false, "This is a comment."]
public string CUST_SEQ { get; set;}
''''


##ColumnNameAttribute

用來宣告Property對應的資料庫欄位名字,不必與Property相同.以下範例為MyProperty對應資料庫的MyDbColumnName的欄位.

''''
[ColumnName("MyDbColumnName")]
public string MyProperty { get; set;}
''''
