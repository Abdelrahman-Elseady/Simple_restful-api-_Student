# Student Management RESTful API

This project is a **Student Management RESTful API** built with **.NET Core Web API** and **SQL Server**.  
It allows full **CRUD operations**, retrieving **passed students**, **calculating average grades**, and **finding students** by ID.

---

## Features

- **Create** a new student
- **Find** student by ID
- **Update** student details
- **Delete** a student
- **Get** all and passed students
- **Calculate** the average grade for all students

---

## Technologies Used

- **Backend Framework**: .NET Core Web API
- **Database**: Microsoft SQL Server
- **Data Access**: ADO.net

---

## API Endpoints

| Method | Route | Description |
|:------:|:------|:------------|
| `GET` | `/api/Students/All` | Get all students |
| `GET` | `/api/Students/Passed` | Get all passed students |
| `GET` | `/api/Students/Average` | Get the average grade of all students |
| `GET` | `/api/Students/{id}` | Get a specific student by ID |
| `POST` | `/api/Students/AddNewStudent` | Create a new student |
| `PUT` | `/api/Students/{id}` | Update a student's information |
| `DELETE` | `/api/Students/{id}` | Delete a student by ID |
