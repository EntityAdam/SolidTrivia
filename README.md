# Jeoparady!

A trivia game loosley resembling a popular game show.

# Table of contents

1. Introduction
    1. Privacy
2. Gameplay
    1. Roles
    1. [Host] Start New Game
    1. [Participant] Join Game
    1. [Host] Answer selection
    1. [Participant] Answer response 
    1. [Participant] Leave game
3. Host your own
    1. Twilio
    1. App Configuration
    1. Answer / Response
4. Answer / Response contributions
5. Developer contributions
6. Roadmap 
7. License

## 1. Introduction

1. This game was designed specifically for coding trivia, usually in a venue with a large display or projector. If you'd like to use this in any other capacity your mileage may vary. This game is designed to be hosted by a single person in a venue with many participants. There is no limit on the number of participants other than network and server capability. Participants join, leave and respond to answers by standard SMS text messaging.

    1. Privacy: This game was designed with privacy in mind.  While you do need to interact with the game with an actual SMS capable device with a valid phone number, this number is not exposed at any point during the game, nor is it stored in any manner (including application logs).  While you are participating in a session, the game assigns you a unique ID which will be sent to you by SMS and can be used to identify yourself on the score board.

## 2. Gameplay

1. Roles
    1. Host: The host is to be the sole operator of the gameboard. The host explains the rules, starts and stops the game, navigates between the game board, answer, response and leaderboard screens.  Most importantly the host should be engaging the audience to make this a fun experience. 
    1. Participants: Participants interact with the game via standard SMS messaging from any carrier service. Standard text messaging rates apply.  Participants may join and leave an in progress game and supply responses to the answers displayed.
1. Start a new game
    - Host should click 'Host New Game' button.
1. Join game
    - Participants must send a text message with the `JOIN <session>` to the number displayed on the Lobby screen. The message is case insensitive and you may omit the hyphen example; `join jumpy-cat` or `join jumpy cat` will both work.
    - As the participants are joining the game, they will automatically show up in the Lobby screen.
    - When participants have joined, click the Start button to proceed to the game board.
    - Participants may still join the game after the Start button has been pressed.
1. Game Board
    - On the game board there are 6 categories and 5 items in each category progressing in value and difficulty.
    - Each board item is presented in the form of an answer, and responses should be in the form of a question.
1. Answer Selection
    - A player will be chosen at random to pick the first question on the answer board.  Their card on the leaderboard will have a green background.
    - For subsequent answers, the first player to respond correctly to the current answer will select the next answer from the board.
1. Answer response
    - Responses will be sent by the participants by SMS messaage. No command is required. Send the response only.
    - Responses should be in the form of a question, however for the sake of brevity, the pronouns are implicitly omitted.
1. Leave game
    - Participants may leave the game at any time by sending the SMS command `LEAVE'.
    - All data is removed from the game and your score is forefit.
    - You may rejoin the game at any time.

## 3. Host your own
    - Coming soon

## 4. Answer / Response contributions
    - Writing good questions and answers is hard work! Answer / Response contributions are appreciated. 
    - Sample spreadsheet format (coming soon)

## 5. Developer contributions
    - Developer contributions are encouraged!
    - Technologies used
        - Language C# 7.3
        - .NET Core 3.0
        - ASP.NET Core
        - Blazor

## 6. Roadmap 
    - The roadmap is just a big list of todo's for now.
    - TODO:
        - [ ] All responses removed when player leaves
        - [ ] Select random player to select first answer
            - [ ] Add CSS class to change the selected user to green.
            - [ ] Determine user with fastest correct response for subequent answers
        - [ ] Import question / response file?
        - [ ] Move phone number / Twilio API keys to secrets
        - [ ] Implement Twilio client to allow server to send SMS messages to users.
