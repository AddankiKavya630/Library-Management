using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryMgmt
{
    internal class DbOperations
    {
        //Create new authors
        internal string AddAuthor()
        {
            Author author = new Author();

            Console.Write("Enter AuthorId :");
            author.AuthorId = int.Parse(Console.ReadLine());

            Console.Write("Enter Author Name : ");
            author.Name = Console.ReadLine();

            Console.Write("Enter Author Email : ");
            string email = Console.ReadLine();

            if (!new EmailAddressAttribute().IsValid(email))
            {
                //Console.WriteLine("Invalid Email Adress ");
                return "Can't able to add author Due to Invalid Email";
            }
            author.Email = email;
            try
            {
                using (LibraryContext context = new LibraryContext())
                {
                    context.Authors.Add(author);
                    context.SaveChanges();
                }
                return "Successfully Added Author";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);

                return ex.Message;
            }
        }

        internal List<Author> GetAuthors()
        {
            List<Author> authors = new List<Author>();
            using (LibraryContext context = new LibraryContext())
            {
                authors = context.Authors.Select(author => author).ToList();
            }

            return authors;
        }

        internal void AddBooksToAuthors()
        {
            List<Author> authors = GetAuthors();
            Console.WriteLine("Author ID      Author Name   Author Email");
            foreach (Author author in authors)
            {
                Console.WriteLine($"{author.AuthorId}, {author.Name}, {author.Email}");
            }

            bool addAuthorBooks = true;

            while (addAuthorBooks)
            {
                Console.WriteLine("Enter the Author ID for adding associated books:");
                int authorId = int.Parse(Console.ReadLine());

                bool addBook = true;

                while (addBook)
                {
                    Book book = new Book();
                    book.AuthorId = authorId;

                    Console.Write("Enter Book ID: ");
                    book.BookId = int.Parse(Console.ReadLine());

                    Console.Write("Enter Book Title: ");
                    book.Title = Console.ReadLine();

                    Console.Write("Enter Book Published Year: ");
                    book.YearPublished = int.Parse(Console.ReadLine());

                    try
                    {
                        using (LibraryContext context = new LibraryContext())
                        {
                            context.Books.Add(book);
                            context.SaveChanges();
                            Console.WriteLine("Book added successfully.");
                        }
                    }
                    catch (DbUpdateException ex)
                    {
                        if (ex.InnerException is SqlException sqlEx)
                        {
                            if (sqlEx.Number == 547)
                            {
                                Console.WriteLine("No author found with the specified ID. Please verify the ID or add the author.");
                                return;
                            }
                            else if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                            {
                                Console.WriteLine("A book with the specified ID already exists.");
                            }
                            else
                            {
                                Console.WriteLine("SQL Error: " + sqlEx.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Unable to add the book: " + ex.Message);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Unexpected error: " + ex.Message);
                    }

                    Console.Write("Do you want to add another book? Enter 1 for Yes, or 0 for No: ");
                    addBook = Console.ReadLine() == "1";
                }

                Console.Write("Do you want to add books for another author? Enter 1 for Yes, or 0 for No: ");
                addAuthorBooks = Console.ReadLine() == "1";
            }
        }


        //List all books with authors
        internal  void ListAllBooks()
        {


            using (LibraryContext context = new LibraryContext())
            {

                var books = context.Books.ToList();
                foreach (var book in books)
                {

                    int aid = book.AuthorId;
                    var author = context.Authors.Find(aid);
                    Console.WriteLine(book.BookId + "   " + book.Title + "   "
                        + book.YearPublished + "  "
                        + author.AuthorId + "  "
                        + author.Name + "  "
                        + author.Email);
                }
            }
        }

        internal void DisplayBooks()
        {
            using (LibraryContext context = new LibraryContext())
            {
                var books = context.Books.ToList();
                Console.WriteLine("Book Id              Title       Published year       Author Id");
                foreach(var book in books)
                {
                    Console.WriteLine($"{book.BookId} {book.Title} {book.YearPublished} {book.AuthorId}");
                }
            }
        }
        //Update a book’s title
        internal void UpdateBookTitle() {

            Console.WriteLine("Availble Book details : ");

            DisplayBooks();

            Console.WriteLine("Enter BookId to update");

            int bookId = int.Parse(Console.ReadLine());

            using (LibraryContext context = new LibraryContext())

            {

                bool exists = context.Books.Any(e => e.BookId == bookId);
                if (!exists)
                {
                    Console.WriteLine("Invalid Book Id");
                    return;
                }

            }

            Console.WriteLine("Enter Title : ");

            string title = Console.ReadLine();
            using (LibraryContext context = new LibraryContext())
            {
                try
                {
                    var emp = context.Books.FirstOrDefault(e => e.BookId == bookId);
                    if (emp != null)
                    {
                        emp.Title = title;
                    }
                    context.SaveChanges();
                    Console.WriteLine("Book Record Updated Succesfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Updation Issue occured ",ex.Message);

                }

            }

        }

        //Delete a book
        public void deleteBook()
        {
            Console.WriteLine("Availble Book details : ");
            DisplayBooks();

            Console.WriteLine("Enter BookId to be delet : ");

            int bookId = int.Parse(Console.ReadLine());

            using (LibraryContext context = new LibraryContext())
            {
                bool exists = context.Books.Any(e => e.BookId == bookId);
                if (!exists)
                {
                    Console.WriteLine("There is no Book Id with this");
                    return;

                }

            }

            using (LibraryContext context = new LibraryContext())
            {
                var emp = context.Books.FirstOrDefault(e => e.BookId == bookId);
                if (emp != null)
                {
                    context.Books.Remove(emp);
                }
                context.SaveChanges();
                Console.WriteLine("Deleted Successfully");

            }

        }
    }
}
