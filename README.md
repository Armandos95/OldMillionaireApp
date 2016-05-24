# OldMillionaireApp
Here lies the Millionaire app made for interactive game written on C# language

This application was intended for using in Millionaire-like interactive game called "Розумники" in my university (CHNUBKh). 

The application can be used for playing both FFF ("Faster Finger First") and normal game mode.
Normal game mode has 15 questions and 3 lifelines, just like on the TV. 
In the beginning player will be able to choose one lifeline from each of the three pairs.
Pair 1: "50:50" or "Double Dip"

Pair 2: "Call a Friend" or "Call an Expert"

Pair 3: "Switch to a question from the same category" or "Switch to a question from some other random category"


It's not yet ready for use because there no questions written, only placeholders in their stead (like "question_5_math").

Questions are written into TXT-files which are allocated into separate folders according to difficulty level (from 1 to 15) and category (18 categories). 
Questions in those TXT-files are saved in such format:

[question text];[option A text];[option B text];[option C text];[option D text];[correct option letter];[first option removed by 50:50 lifeline letter];[second option removed by 50:50 lifeline letter];[question type]

The [question type] field can contain one of three strings: "text", "audio", "photo". 
The question of "text" type contains nothing but text.
The "audio" type question also supposed to have an attached audio-file that is located in "audio" directory and can be played when this question pops up after pressing the special button on the form.
Likewise, the "photo" type question has an attached picture located in the "photos" folder.

Amount of categories and their names are fixed. However, the settings.ini file can be used for changing the amount of questions on each level and category and also change the amount of FFF questions.
