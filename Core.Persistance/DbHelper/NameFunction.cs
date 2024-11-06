using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;

namespace Core.Persistance.DbHelper;

public static class NameFunction
{
    private static string harfUpper(string data) => data.ToUpper().Replace("İ", "I");
    private static string harfLower(string data) => data.Replace("I", "i").ToLower();
    public static void NameUpper(this ModelBuilder modelBuilder, DataBaseEnums dataBaseEnums)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            OracleNameUpdated(entity, dataBaseEnums);
            PostgreNameUpdated(entity, dataBaseEnums);

        }
    }
    static void OracleNameUpdated(IMutableEntityType? entity, DataBaseEnums dataBaseEnums)
    {
        if (dataBaseEnums == DataBaseEnums.Oracle)
        {
            entity.SetTableName(harfUpper(entity.GetTableName()));
            foreach (var item in entity.GetProperties())
                item.SetColumnName(harfUpper(item.GetDefaultColumnBaseName()));
        }
    }
    static void PostgreNameUpdated(IMutableEntityType? entity, DataBaseEnums dataBaseEnums)
    {
        if (dataBaseEnums == DataBaseEnums.PostgreSql)
        {
            entity.SetTableName(harfLower(entity.GetTableName()));
            foreach (var item in entity.GetProperties())
                item.SetColumnName(harfLower(item.GetDefaultColumnBaseName()));
        }
    }
}
