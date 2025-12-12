# TodoList Project

A console-based Todo List application built with C# and .NET 10 that helps you organize and manage your tasks efficiently.

This project is a Lexicon assignment.

## Overview

This TodoList application provides a simple yet powerful way to manage your daily tasks. You can create todos, organize them into projects, set due dates, mark them as completed, and view them in various ways. All your data is automatically saved to JSON files for persistence between sessions.

## Features

- **Task Management**: Create, edit, and remove todos
- **Project Organization**: Group related todos into projects
- **Multiple View Options**: View todos by date, by project, or filter by completion status
- **Persistent Storage**: Automatically saves your data using JSON file storage
- **User-Friendly Interface**: Color-coded console interface for better readability

## Getting Started

### Prerequisites

- .NET 10 SDK or later

### Running the Application

1. Clone the repository
2. Run the application

## How to Use

### Main Menu

When you start the application, you'll see a welcome message showing your task statistics and the main menu with these options:

1. **Show Todo List** - View your todos in different formats
2. **Add New Todo** - Create a new task
3. **Edit Todo** - Update, mark as completed, or remove existing tasks
4. **Save and Quit** - Save your changes and exit the application

### Viewing Your Todos

Select option `1` from the main menu to access the Todo List Menu:

1. **Sort by date** - View all todos ordered by due date
2. **Split by project** - View todos grouped under their respective projects
3. **Show incomplete** - View only tasks that are not yet completed
4. **Show completed** - View only finished tasks
5. **Return to main menu** - Go back to the main menu

### Adding a New Todo

Select option `2` from the main menu to add a new todo:

- Enter or select a project name (you can create a new project or use an existing one)
- Enter the todo title
- Enter the due date in the format shown
- Optionally add another todo or return to the main menu

### Editing Todos

Select option `3` from the main menu to access the Edit Todo Menu:

- The application will display all todos with their IDs
- Choose an edit option:
1. **Update Todo Details** - Change title, due date, project, or completion status
2. **Mark as Completed** - Mark a todo as done
3. **Remove Todo** - Delete a todo (you'll be asked to confirm)
4. **Return to Main Menu** - Go back to the main menu

### Saving Your Work

- Select option `4` from the main menu to save all changes and exit
- Your todos and projects are saved to JSON files in the application directory

### Canceling Operations

- Type `Q` at any input prompt (not menus) to cancel the current operation and return to the main menu

## Data Storage

The application stores data in JSON format:
- Todos and projects are saved automatically when you exit
- Data is loaded automatically when you start the application
- All data files are stored in the application directory

## Tips

- **Projects are created automatically** when you add a todo with a new project name
- **Projects are removed automatically** when they have no associated todos

## Development

This project is built using:
- **Language**: C# 14.0
- **Framework**: .NET 10
- **Architecture**: Object-oriented with separation of concerns (UI, Business Logic, Data Storage)

## Repository

GitHub: [https://github.com/arden-rh/lexicon-todolist-project](https://github.com/arden-rh/lexicon-todolist-project)

