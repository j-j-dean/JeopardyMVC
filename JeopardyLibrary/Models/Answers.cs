/*********************************************************************
 * 
 * JeopardyLibrary - Answers class
 * 
 * This class contains getters and setters for the Entity Framework
 * references to the Answers table:
 * 
 *     QuestionID    - Integer value representing the table ID of the
 *                     corresponding entry in the Questions table
 *     Answer        - Character string answer to the related question
 *     CorrectAnswer - Boolean, true/false, is this answer correct?
 *     Question      - Reference to the corresponding entry in the
 *                     Questions table
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
     * Answers class is used for Answers table entry in the database
     * model to indentify the Answer to the question that
     * is referenced via the QuestionId via Foreign Key reference
     *********************************************************/
    public class Answers: EntityBase
    {
        public int QuestionId { get; set; } // Question Reference Id
        [StringLength(50)]
        public String Answer { get; set; } // Jeopardy Question's Answer
        public Boolean CorrectAnswer { get; set; } // Is it correct?
        [ForeignKey(nameof(QuestionId))]
        public Questions Question { get; set; } // Question Reference
    }
}
