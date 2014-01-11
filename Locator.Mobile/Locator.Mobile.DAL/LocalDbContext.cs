using System;
using System.IO;
using Locator.Mobile.DAL.Tables;
using SQLite;

namespace Locator.Mobile.DAL
{
    public class LocalDbContext
    {
        private const string DatabaseName = "CommitDogDB";
        private readonly SQLiteConnection db;

        public SQLiteConnection Db
        {
            get { return db; }
        }

        public LocalDbContext()
        {
            string databasePath = string.Empty;
#if WINDOWS_PHONE

#else
            databasePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
#endif
            db = new SQLiteConnection(Path.Combine(databasePath, DatabaseName), SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite);

            RegisterTables();
        }

        /// <summary>
        /// Создание таблиц
        /// </summary>
        private void RegisterTables()
        {
            db.CreateTable<Requests>();
            //db.CreateTable<LocatorAction>();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
