using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLiteLivro
{
    public interface lAcessoDB
    {
        SQLiteAsyncConnection GetConnection();
    }
}
