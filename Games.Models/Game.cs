using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Models
{
    public class Game
    {
        public int Id { get; set; }

        //основні поля
        public string Name { get; set; } = string.Empty;   
        public string Studio { get; set; } = string.Empty;    
        public string Genre { get; set; } = string.Empty;     
        public DateTime ReleaseDate { get; set; }           

        //нові поля
        public string GameMode { get; set; } = string.Empty;  
        public int CopiesSold { get; set; }                 
    }
}
