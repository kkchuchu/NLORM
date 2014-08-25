# NLORM, A Lightweight ORM For C# .Net 

## Introduction

NLORM(dotNet Lightweight ORM)是一個基於C# .Net實作的一個輕量級
ORM Framework。主要目的在於減輕程式設計師過去要將Table轉換為物件
時的負擔。

NLORM基於1-class對應1-table的概念進行設計。為了達到易於使用的以及
高度的擴充性。NLORM所提供的每個功能都可以被單獨使用。

## Download

## Examples

### Get NLORMDb
在NLROM中必須取得一個NLROMDb來對DB進行操作。可以藉由NLORMFactroy來取
得一個NLORM支援的DB。更詳細的操作請參考文件[NLORM](/nlrom/)章節。

```
//取得一個連線至MsSql Server的NLORMDb
var connectionStr = "IamConnectionString";
INLORMDb db = NLORM.Manager.GetDb(connectionStr, SupportedDb.MSSQL);
```

```
//取得一個Sqlite的NLORMDb
var connectionStr = "Data Source=C:\\test.sqlite";
INLORMDb db = NLORM.Manager.GetDb(connectionStr, SupportedDb.SQLITE);
```

### Model
NLORM採用1-class對應1-table的設計原則。所以每個table的column需對應到一個
Model class中的property。而Model Class對應Table column的規則在NLORM中可以
透過多種不同的Attribute來設定。更詳細的定義請參考文件[Model](/model/)章節。


以下範例定義了一個User的Model。他有Id、Name、Email、Birth等屬性
```
//一個User的Model務件
public class User
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime Birth { get; set; }
}
```


但是許多時候Class Name不一定就是資料庫的Table Name。而Model class的property name
在DB中的column name也不一定一致。所以NLORM中提供了幾個attrtibute來做命名上的對應。
```
//命名範例
[TableName("UUSER")]
public class User
{
    [ColumnName("UID")]
    public string ID { get; set; }

    [ColumnName("UNAME")]
    public string Name { get; set; }

    public string Email { get; set; }

    public DateTime Birth { get; set; }
}
```

而在使用NLORM幫Model Class建立Table時還可以使用ColumnType Attribute協助建立一些DB的
屬性。

```
[TableName("UUSER")]
public class User
{
    [ColumnName("UID")]
    [ColumnType(DbType.String, "10", false, "User Id")]
    public string ID { get; set; }

    [ColumnName("UNAME")]
    [ColumnType(DbType.String, "30", false, "User Email")]
    public string Name { get; set; }

    public string Email { get; set; }

    public DateTime Birth { get; set; }
}
```

### Create Table
利用NLORMDb建立一個User Model的Table。


詳細的使用方法請參考文件[CRUD](/crud/)章節。

```
INLORMDb db = NLORM.Manager.GetDb(connectionStr, SupportedDb.SQLITE);
db.CreateTable<User>();
```

### Insert Model

詳細的使用方法請參考文件[CRUD](/crud/)章節。


```
var user1 = new User { ID = "135", Name = "Nlorm", Email = "nlrom@is.good", Birth = DateTime.Now };
db.Insert<User>(user1);
```

### Query
在Query的方法中，NLORM提供了FilterBy方法來過慮查詢結果。預設回傳傳入Model的List。

詳細的使用方法請參考文件[CRUD](/crud/)章節。

```
//Select All
INLORMDb db = NLORM.Manager.GetDb(connectionStr, SupportedDb.SQLITE);
var users = db.Query<User>();
```

```
//Select Id is 135
INLORMDb db = NLORM.Manager.GetDb(connectionStr, SupportedDb.SQLITE);
var users = db.FilterBy(FilterType.EQUAL_AND,new { ID="135"}).Query<User>();
```

```
//Select Id is 135 and Name is "Nlorm"
INLORMDb db = NLORM.Manager.GetDb(connectionStr, SupportedDb.SQLITE);
var users = db.FilterBy(FilterType.EQUAL_AND,new { ID="135",Name="Nlorm"}).Query<User>();
```

```
//Select Id is 135 OR Name is "Nlorm"
INLORMDb db = NLORM.Manager.GetDb(connectionStr, SupportedDb.SQLITE);
var users = db.FilterBy(FilterType.EQUAL_OR,new { ID="135",Name="Nlorm"}).Query<User>();
```

```
//Select Id is 135 AND Id is 136
INLORMDb db = NLORM.Manager.GetDb(connectionStr, SupportedDb.SQLITE);
var users = db.FilterBy(FilterType.EQUAL_AND, new { ID = "135" })
    .And().FilterBy(FilterType.EQUAL_AND, new { ID="136"})
    .Query<User>();
```

### Delete
刪除資料。用法跟Query類似，搭配FilterBy來過慮要刪除的資料。

詳細的使用方法請參考文件[CRUD](/crud/)章節。
```
//Delete Id is 135
INLORMDb db = NLORM.Manager.GetDb(connectionStr, SupportedDb.SQLITE);
var users = db.FilterBy(FilterType.EQUAL_AND,new { ID="135"}).Delete<User>();
```

```
//Select Id is 135 and Name is "Nlorm"
INLORMDb db = NLORM.Manager.GetDb(connectionStr, SupportedDb.SQLITE);
var users = db.FilterBy(FilterType.EQUAL_AND,new { ID="135",Name="Nlorm"}).Delete<User>();
```

### Update
更新資料。搭配FilterBy來篩選要更新的Model。

詳細的使用方法請參考文件[CRUD](/crud/)章節。
```
//Update Id is 135 to New Name
var newUser= new User { ID = "135", Name = "Nlorm New", Email = "nlrom@is.good", Birth = DateTime.Now };
INLORMDb db = NLORM.Manager.GetDb(connectionStr, SupportedDb.SQLITE);
db.FilterBy(FilterType.EQUAL_AND,new { ID="135"}).Update<User>(newUser);
```
或使用匿名型別

```
//Update Id is 135 to New Name
INLORMDb db = NLORM.Manager.GetDb(connectionStr, SupportedDb.SQLITE);
db.FilterBy(FilterType.EQUAL_AND,new { ID="135"}).Update<User>(new {Name = "Nlorm New"});
```

### Transaction
在NLORM中使用Transaction

```
INLORMDb db = NLORM.Manager.GetDb(connectionStr, SupportedDb.SQLITE);
db.CreateTable<TestClassUser>();
var trans = sqliteDbc.BeginTransaction();
var testObj = new TestClassUser();
testObj.ID = 1;
testObj.Name = "Name " + 1;
testObj.CreateTime = DateTime.Now;
sqliteDbc.Insert<TestClassUser>(testObj);
trans.Commit();  // or Rollback()
sqliteDbc.Close();
```

## Contributing

Feel free to folk,and send me pull request.


