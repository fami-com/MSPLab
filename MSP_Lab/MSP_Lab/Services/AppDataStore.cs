using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MSP_Lab.Data;
using MSP_Lab.Models;
using SQLite;
using System.Linq;

namespace MSP_Lab.Services
{
    public class AppDataStore
    {
        private string _dbPath;
        private SQLiteAsyncConnection _db;

        public static async Task<AppDataStore> Create()
        {
            var ds = new AppDataStore();
            await ds.Configure();
            return ds;
        }

        private AppDataStore()
        {
            _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Database.db");
            _db = new SQLiteAsyncConnection(_dbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache | SQLiteOpenFlags.ReadWrite);
        }

        private async Task Configure()
        {
            await _db.CreateTablesAsync<Book, BookDetails, RemoteImage>();
        }

        public async Task InsertBookAsync(Book item)
        {
            await _db.InsertOrReplaceAsync(item);
        }

        public async Task InsertAllBooksAsync(IEnumerable<Book> items)
        {
            foreach(var b in items)
            {
                await InsertBookAsync(b);
            }
        }

        public async Task InsertDetailsAsync(BookDetails item)
        {
            await _db.InsertOrReplaceAsync(item);
        }

        public async Task InsertImageAsync(RemoteImage item)
        {
            await _db.InsertOrReplaceAsync(item);
        }

        public async Task InsertAllImagesAsync(IEnumerable<RemoteImage> items)
        {
            foreach(var i in items)
            {
                await InsertImageAsync(i);
            }
        }

        public async Task<Book> GetBookByIdAsync(string id)
        {
            try
            {
                return await _db.GetAsync<Book>(id);
            }
            catch
            {
                return default;
            }
        }

        public async Task<BookDetails> GetDetailsByIdAsync(string id)
        {
            try
            {
                return await _db.GetAsync<BookDetails>(id);
            }
            catch
            {
                return default;
            }
        }

        public async Task<IEnumerable<Book>> GetBooksBySearchStringAsync(string search)
        {
            //return await _db.Table<Book>().Where(b => b.Title.Contains(search, StringComparison.InvariantCultureIgnoreCase)).ToArrayAsync();
            return await _db.Table<Book>().Where(b => b.Title.ToLower().Contains(search.ToLower())).ToArrayAsync();
        }

        public async Task<IEnumerable<RemoteImage>> GetImagesBySearchStringAsync(string search)
        {
            return await _db.Table<RemoteImage>().Where(b => b.Search.ToLower().Contains(search.ToLower())).ToArrayAsync();
        }
    }
}
