using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMgmt
{
    internal class Book
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BookId {  get; set; }
        public string Title { get; set; }
        public int YearPublished {  get; set; }
        public int AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public Author author { get; set; }
        
    }
}
