/*********************************************************************
 * 
 * JeopardyLibrary - Questions class
 * 
 * This class contains getters and setters for the Entity Framework
 * references to the Questions table:
 * 
 *     CategoryId    - Integer value representing the table ID of the
 *                     corresponding entry in the Categories table
 *     Question      - Character string question for the related category
 *     PointValue    - Integer value representing the point value for this
 *                     question.
 *     Categories    - Reference to the corresponding entry in the
 *                     Categories table
 * 
 *********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JeopardyLibrary.Models.Base;

namespace JeopardyLibrary.Models
{
    /**********************************************************
     * Questions class is used for Questions table entry in the database
     * model to indentify a Question that corresponds to a category
     * referenced via the CategoryId via Foreign Key reference
     *********************************************************/
    public class Questions: EntityBase
    {
        public int CategoryId { get; set; } // Category Reference Id
        [StringLength(50)]
        public string Question { get; set; } // Jeopardy Question
        public int PointValue { get; set; } // Jeopardy Question's Point Value
        [ForeignKey(nameof(CategoryId))]
        public Categories Categories { get; set; } // Category Reference
    }
}
