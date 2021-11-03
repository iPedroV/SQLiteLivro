using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLiteLivro
{
    interface lAcessoDB
    {
        SQLiteAsyncConnection GetConnection();
    }
}
