using SQLite;
using SQLiteLivro.AcessoDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SQLiteLivro.model;


[assembly: Xamarin.Forms.Dependency(typeof(AcessoDB))]
namespace SQLiteLivro.AcessoDB
{
    class AcessoDB : lAcessoDB
    {
        public SQLiteAsyncConnection GetConnection()
        {
            string nomeDB = "Livro.db";
            var caminhoPasta =
                System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.MyDocuments);
            var caminho = Path.Combine(caminhoPasta, nomeDB);
            return new SQLiteAsyncConnection(caminho);
        }
    }
}
