# Basic Chatbot with Machine Learning (WPF/C#)

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## BOOKMARKS
- [Objective](#objective)
- [Features](#key-features)
- [Installation](#installation-instructions)
- [License](#license)
- [References](#references)

## OBJECTIVE

### PURPOSE
This project implements a WPF-based chatbot application in C# 
that utilizes machine learning for response generation. The 
application features a minimalist UI design and learns from 
user interactions to improve its conversational abilities over time.

Date of creation: 2025-07  
Technical: C# 7.0, WPF, ML.NET, .NET 6  
Laboratory: Visual Studio 2022

## INSTALLATION INSTRUCTIONS

GitHub Repo: [basic-chatbot-cs](https://github.com/Naoyuki-Christopher-H/basic-chatbot-cs.git)

1. Clone the repository:
   ```
   git clone https://github.com/Naoyuki-Christopher-H/basic-chatbot-cs.git
   ```

2. Open the solution in Visual Studio 2022

3. Restore NuGet packages:
   ```
   dotnet restore
   ```

4. Build the project:
   ```
   dotnet build
   ```

5. Run the application:
   ```
   dotnet run
   ```

## KEY FEATURES

- Machine learning-powered responses using ML.NET
- Conversation logging to text files
- Minimalist UI design
- Continuous learning from interactions
- Teaching mode for response improvement

## FILE STRUCTURE

```
basic-chatbot-cs/
├── App.xaml
├── App.xaml.cs
├── MainWindow.xaml
├── MainWindow.xaml.cs
├── ChatTrainingService.cs
├── ChatMessageModel.cs
├── assets/
└── README.md
```

Developed using WPF and ML.NET with C# experience.  
Achieved functional chatbot with learning capabilities.

## LICENSE

License Name: MIT License  
Role: Permits free use, modification, and distribution with attribution.  
License File: [FULL LICENSE](LICENSE)

## REFERENCES

IF THIS REPOSITORY IS USED, PLEASE USE THIS TEMPLATE AS A REFERENCE:

> Author(s). (Year). *Title of Repository*. Available at: \[URL] (Accessed: \[Date]).

## DISCLAIMER

UNDER NO CIRCUMSTANCES SHOULD IMAGES OR EMOJIS BE INCLUDED DIRECTLY IN 
THE README FILE. ALL VISUAL MEDIA, INCLUDING SCREENSHOTS AND IMAGES OF 
THE APPLICATION, MUST BE STORED IN A DEDICATED FOLDER WITHIN THE PROJECT 
DIRECTORY. THIS FOLDER SHOULD BE CLEARLY STRUCTURED AND NAMED ACCORDINGLY 
TO INDICATE THAT IT CONTAINS ALL VISUAL CONTENT RELATED TO THE APPLICATION 
(FOR EXAMPLE, A FOLDER NAMED IMAGES, SCREENSHOTS, OR MEDIA).

I AM NOT LIABLE OR RESPONSIBLE FOR ANY MALFUNCTIONS, DEFECTS, OR ISSUES THAT 
MAY OCCUR AS A RESULT OF COPYING, MODIFYING, OR USING THIS SOFTWARE. IF YOU 
ENCOUNTER ANY PROBLEMS OR ERRORS, PLEASE DO NOT ATTEMPT TO FIX THEM SILENTLY 
OR OUTSIDE THE PROJECT. INSTEAD, KINDLY SUBMIT A PULL REQUEST OR OPEN AN ISSUE 
ON THE CORRESPONDING GITHUB REPOSITORY, SO THAT IT CAN BE ADDRESSED APPROPRIATELY 
BY THE MAINTAINERS OR CONTRIBUTORS.
