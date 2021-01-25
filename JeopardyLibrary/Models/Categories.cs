/*********************************************************************
 * 
 * JeopardyLibrary - Categories class
 * 
 * This class contains getters and setters for the Entity Framework
 * references to the Categories table:
 * 
 *     Name   - the name of the Category
 *     Active - Boolean, true/false, is this category selected to be
 *              an active category for game play? (active categories
 *              are placed in the game during game play)
 * 
 *********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using JeopardyLibrary.Models.Base;

namespace JeopardyLibrary.Models
{
    /*********************************************************************
     * Categories class is used for Categories table entry in the database
     * model to indentify the possible Question categories
     *********************************************************************/
    public class Categories: EntityBase
    {
        [StringLength(50)]
        public string Name { get; set; } // Category Name
        public Boolean Active { get; set; } // Active for Game Play?
    }
}
