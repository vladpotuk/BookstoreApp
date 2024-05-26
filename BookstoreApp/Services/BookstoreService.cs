using System;
using System.Collections.Generic;
using System.Data.SQLite;

public class BookstoreService
{
    private readonly DatabaseContext _dbContext;

    public BookstoreService(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddBook(Book book)
    {
        _dbContext.OpenConnection();
        string query = "INSERT INTO Books (Title, Author, Publisher, Pages, Genre, PublicationYear, Cost, Price, IsContinuation) VALUES (@Title, @Author, @Publisher, @Pages, @Genre, @PublicationYear, @Cost, @Price, @IsContinuation)";
        using (var cmd = new SQLiteCommand(query, _dbContext.Connection))
        {
            cmd.Parameters.AddWithValue("@Title", book.Title);
            cmd.Parameters.AddWithValue("@Author", book.Author);
            cmd.Parameters.AddWithValue("@Publisher", book.Publisher);
            cmd.Parameters.AddWithValue("@Pages", book.Pages);
            cmd.Parameters.AddWithValue("@Genre", book.Genre);
            cmd.Parameters.AddWithValue("@PublicationYear", book.PublicationYear);
            cmd.Parameters.AddWithValue("@Cost", book.Cost);
            cmd.Parameters.AddWithValue("@Price", book.Price);
            cmd.Parameters.AddWithValue("@IsContinuation", book.IsContinuation ? 1 : 0);
            cmd.ExecuteNonQuery();
        }
        _dbContext.CloseConnection();
    }

    public void RemoveBook(string title)
    {
        _dbContext.OpenConnection();
        string query = "DELETE FROM Books WHERE Title = @Title";
        using (var cmd = new SQLiteCommand(query, _dbContext.Connection))
        {
            cmd.Parameters.AddWithValue("@Title", title);
            cmd.ExecuteNonQuery();
        }
        _dbContext.CloseConnection();
    }

    public void UpdateBook(string title, Book updatedBook)
    {
        _dbContext.OpenConnection();
        string query = "UPDATE Books SET Author = @Author, Publisher = @Publisher, Pages = @Pages, Genre = @Genre, PublicationYear = @PublicationYear, Cost = @Cost, Price = @Price, IsContinuation = @IsContinuation WHERE Title = @Title";
        using (var cmd = new SQLiteCommand(query, _dbContext.Connection))
        {
            cmd.Parameters.AddWithValue("@Author", updatedBook.Author);
            cmd.Parameters.AddWithValue("@Publisher", updatedBook.Publisher);
            cmd.Parameters.AddWithValue("@Pages", updatedBook.Pages);
            cmd.Parameters.AddWithValue("@Genre", updatedBook.Genre);
            cmd.Parameters.AddWithValue("@PublicationYear", updatedBook.PublicationYear);
            cmd.Parameters.AddWithValue("@Cost", updatedBook.Cost);
            cmd.Parameters.AddWithValue("@Price", updatedBook.Price);
            cmd.Parameters.AddWithValue("@IsContinuation", updatedBook.IsContinuation ? 1 : 0);
            cmd.Parameters.AddWithValue("@Title", title);
            cmd.ExecuteNonQuery();
        }
        _dbContext.CloseConnection();
    }

    public List<Book> SearchBooksByTitle(string title)
    {
        _dbContext.OpenConnection();
        string query = "SELECT * FROM Books WHERE Title LIKE '%' || @Title || '%'";
        using (var cmd = new SQLiteCommand(query, _dbContext.Connection))
        {
            cmd.Parameters.AddWithValue("@Title", title);
            using (var reader = cmd.ExecuteReader())
            {
                List<Book> books = new List<Book>();
                while (reader.Read())
                {
                    Book book = new Book
                    {
                        BookID = Convert.ToInt32(reader["BookID"]),
                        Title = reader["Title"].ToString(),
                        Author = reader["Author"].ToString(),
                        Publisher = reader["Publisher"].ToString(),
                        Pages = Convert.ToInt32(reader["Pages"]),
                        Genre = reader["Genre"].ToString(),
                        PublicationYear = Convert.ToInt32(reader["PublicationYear"]),
                        Cost = Convert.ToDecimal(reader["Cost"]),
                        Price = Convert.ToDecimal(reader["Price"]),
                        IsContinuation = Convert.ToBoolean(reader["IsContinuation"])
                    };
                    books.Add(book);
                }
                return books;
            }
        }
    }
}
