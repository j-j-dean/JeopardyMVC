/*********************************************************************
 * 
 * JeopardyLibrary - EntityBase class
 * 
 * This class contains getters and setters for the common database table 
 * entries that occur in each table (primary key - the table ID)
 * 
 *********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace JeopardyLibrary.Models.Base
{
    /**********************************************************
     * EntityBase is used for each table entry in the database
     * model to indentify a unique ID that is the primary key
     * ********************************************************/
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
