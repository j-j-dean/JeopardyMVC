/*********************************************************************
 *
 * JeopardyMVC - site.js - JavaScript
 *
 * Purpose:
 *     1) When creating new questions and answers a limit selection drop down box
 *        is monitored for updates.
 *     2) During Jeopardy Game Play - game play is controlled here in JavaScript
 * 
 * Functions called include:
 *     LimitSelection - modifies the href for the anchor, <a> element based
 *                      on the user's selection in drop down menu
 *     BackToJeopardy - determines if all questions have been completed.  If 
 *                      complete the CSS display property is set to display the
 *                      completion view, otherwise the game returns to the 
 *                      Jeopardy grid view displaying categories and question 
 *                      point values,
 *     CheckAnswer    - examines the user selection for correctness, stores a
 *                      message for the user to view, and updates the game score
 *     SetTimer       - sets a timer limiting the amount of time to answer a 
 *                      question, stores a message for the user if time has
 *                      expired, and wakes up every second to examine time remaining
 *     AskQuestion    - changes CSS display property to switch from jeopardy grid
 *                      view to question view allowing the user to see the current
 *                      question and answer selections
 *                      
 *     $(document).ready  - When modifying the Jeopardy game data - the limit
 *                          selection portions of the page are updated.
 *                        - When playing Jeopardy - game click handlers are added
 *                          for the point value tiles to load the corresponding
 *                          question, and the play again button action is added
 *                          to reload the page
 *
 *********************************************************************/

// Constants
var jconstants = {
    TOTAL_QUESTIONS: 30,
    BONUS_SCORE: 4000,
    ONE_SECOND_MS: 1000,  // milliseconds
    INITIAL_TIMER_SECS: 45
}

// Jeopardy state variables
var jeopardy = {
    completed_questions: 0,
    your_score: 0,
    time_remaining: 0
};

// Function to set href when limiting selections in view
jeopardy.LimitSelection = function () {

    // Check if view has "limit-by-list" id
    if ($('#limit-by-list').length) {

        // Store controller name from the "view-title" element
        var controller_name = $('#view-title').text().trim();

        // Set href = "/{controller name}/Index/"
        var original_index_href = "/" + controller_name + "/Index/";

        // Update the href attribute when the document is loaded
        $('#limit-by-id').attr("href", original_index_href +
            $('#limit-by-list option:selected').val());

        // Update the href attribute each time the anchor reference is clicked
        $('#limit-by-id').click(function () {
            $('#limit-by-id').attr("href", original_index_href +
                $('#limit-by-list option:selected').val());
        });
    }
}

// Return back to the jeopardy grid
jeopardy.BackToJeopardy = function (msg, correct_answer) {

    // Clear the grid view and replace with the question view
    $('#jeopardy-container').css("display", "block");
    $('#question-box').css("display", "none");

    // Get the category (column #) and question (row #) numbers of the question
    var cnum = $('#the-question').attr("cnum");
    var qnum = $('#the-question').attr("qnum");

    // update the score on the display
    $('#score').text("Your score is " + jeopardy.your_score.toString() + " - " + msg);
    $('#timer').text(correct_answer);

    // clear the last question and point value from the jeopardy grid view - question has been completed
    var $question_entry = $('.question[cnum=' + cnum + '][qnum=' + qnum + ']');
    $question_entry.text("");
    var $points_entry = $('.points[cnum=' + cnum + '][qnum=' + qnum + ']');
    $points_entry.text("");

    // check the first radio button answer as the default in the question and answers view
    $('#answer-a').prop("checked", true);

    // increment the number of completed questions
    jeopardy.completed_questions++;

    // display completion page if all questions completed
    if (jeopardy.completed_questions == jconstants.TOTAL_QUESTIONS) {
 
        $('#score').text(msg + " All complete! Final score: " + jeopardy.your_score.toString());
        var score_text = "<br />Your score - " + parseInt(jeopardy.your_score) + " points";

        /********************************************************
         * Append score to completion view!!!!!!
         ********************************************************/
        $('#completion').find('h2').append(score_text);
        $('#timer').text(correct_answer);

        /********************************************************
         * Display completion message view
         ********************************************************/
        $('#completion-box').css("display", "block");
        $('#jeopardy-container').css("display", "none");
    }

    // clear timeouts when returning to jeopardy grid
    clearTimeout(jeopardy.timeout);
}

// Check the selected answer for correctness (update the score, display message to user)
jeopardy.CheckAnswer = function ($question_element) {

    var msg = "";
    var correct_answer = "";

    // Get the category (column #) and question (row #) numbers of the question
    var cnum = $('#the-question').attr("cnum");
    var qnum = $('#the-question').attr("qnum");
 
    // Get the user's answer selection
    var selection = $('#question-and-answers').find(':checked').val();
    var $answer_entry = $('.answer-' + selection + "[cnum=" + cnum + "][qnum=" + qnum + "]");
 
    // Update the score if the correct answer was given
    if ($answer_entry.attr("correct")) {

        jeopardy.your_score += parseInt($answer_entry.siblings(".points").text().trim());
        msg = "Correct!"
    } else {
        msg = "Try Again!";
        var $correct_entry = $answer_entry.siblings("[correct=correct]");
        correct_answer = "Correct answer: ";
        if ($correct_entry.hasClass("answer-a")) {
            correct_answer += "a) ";
        } else if ($correct_entry.hasClass("answer-b")) {
            correct_answer += "b) ";
        } else if ($correct_entry.hasClass("answer-c")) {
            correct_answer += "c) ";
        } else if ($correct_entry.hasClass("answer-d")) {
            correct_answer += "d) ";
        }
        correct_answer += $correct_entry.text().trim();
    }

    // remove the click handler
    $("#final-answer").off("click", jeopardy.CheckAnswer);

    // return to the jeopardy grid view
    jeopardy.BackToJeopardy(msg, correct_answer);
}

// Timer function to limit amount time to answer question
jeopardy.setTimer = function () {
    if (jeopardy.time_remaining == 0) {

        // Get the category (column #) and question (row #) numbers of the question
        var cnum = $('#the-question').attr("cnum");
        var qnum = $('#the-question').attr("qnum");

        // Get the correct answer for the question
        var $question_entry = $('.question' + "[cnum=" + cnum + "][qnum=" + qnum + "]");
        var $correct_entry = $question_entry.siblings("[correct=correct]");
        var correct_answer = "Correct answer: ";
        if ($correct_entry.hasClass("answer-a")) {
            correct_answer += "a) ";
        } else if ($correct_entry.hasClass("answer-b")) {
            correct_answer += "b) ";
        } else if ($correct_entry.hasClass("answer-c")) {
            correct_answer += "c) ";
        } else if ($correct_entry.hasClass("answer-d")) {
            correct_answer += "d) ";
        }
        correct_answer += $correct_entry.text().trim();

        // return to the jeopardy grid view
        jeopardy.BackToJeopardy("Time expired!", correct_answer);

        // remove the click handler for the submit button
        $("#final-answer").off("click", jeopardy.CheckAnswer);

    } else {

        // update timer message and set new timer
        $('#timer').text("Time remaining: " + jeopardy.time_remaining.toString());
        jeopardy.time_remaining--;
        jeopardy.timeout = setTimeout(function () {
            jeopardy.setTimer();
        }, jconstants.ONE_SECOND_MS);
    }
}

// Replace the jeopardy grid view with the selected question and answers
jeopardy.AskQuestion = function ($question_element) {

    // Skip question boxes that have already been completed (question still in list)
    if ($question_element.text().trim() == "") {
        return;
    }
    // replace the question grid with the question and answers view
    $('#question-box').css("display", "block");
    $('#jeopardy-container').css("display", "none");

    // Add the question to the question and answers view
    $('#the-question').text($question_element.text().trim());

    // set attributes for the category and question for element with id=the-question
    $('#the-question').attr("cnum", $question_element.attr("cnum"));
    $('#the-question').attr("qnum", $question_element.attr("qnum"));

    // Add the answers to the question and answer view
    var $answer = $question_element.next(); // answer-a is next element in document
    $('#answer-a').next().text("a) "+ $answer.text().trim());
    $answer = $answer.next(); // answer-b is next element in document
    $('#answer-b').next().text("b) " + $answer.text().trim());
    $answer = $answer.next(); // answer-c is next element in document
    $('#answer-c').next().text("c) " + $answer.text().trim());
    $answer = $answer.next(); // answer-d is next elemnt in document
    $('#answer-d').next().text("d) " + $answer.text().trim());

    // add the handler for clicking the submit button
    $("#final-answer").on("click", jeopardy.CheckAnswer);

    // set timer to limit amount of time to respond to question
    jeopardy.timeout = setTimeout(function () {
        jeopardy.time_remaining = jconstants.INITIAL_TIMER_SECS;
        jeopardy.setTimer();
    }, jconstants.ONE_SECOND_MS);

}

// Function to perform after the window loads
$(document).ready(function () {

    // Initialize hypertext reference (href) for dropdown selections (if any)
    jeopardy.LimitSelection();

    // Change view to display selected question when clicked
    $(".points").click(function () {
        jeopardy.AskQuestion($(this).next());
    });

    // Reset the page if play-again button pressed
    $('#play-again').click(function () {
        location.reload(true);
    });

 });