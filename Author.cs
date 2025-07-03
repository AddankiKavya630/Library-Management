using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMgmt
{
    [Table("Author")]
    internal class Author
    {
        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AuthorId {  get; set; }

        public string Name {  get; set; }

        public string Email {  get; set; }
    }
}
