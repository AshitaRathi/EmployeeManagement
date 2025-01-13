Employee Management System
The Employee Management System is designed to manage employee details, job roles, and team assignments within an organization. The system allows administrators to view, add, update, and delete employee information, and associate employees with job roles and teams. This project uses Entity Framework for CRUD operations and defines the database schema using DBML (Database Markup Language). Additionally, JWT (JSON Web Token) authentication is implemented to secure user access.

Features
  Manage employee information, including personal details, job roles, and team assignments.
  View employees by their assigned team and job title.
  Add, update, and delete employee records.
  Use Entity Framework for seamless database interaction.
  JWT Authentication is implemented for securing API access.
  Seed data is provided for quick database setup.

Below is the Entity-Relationship (ER) diagram that represents the database structure:
![image](https://github.com/user-attachments/assets/9b438ad2-099d-4d9f-9348-0e51622ae506)

A SQL file (InsertSeedData.sql) is provided to populate the database with initial sample data. 

Database Structure
The database structure consists of three main tables:
Employee: Stores employee details such as name, email, phone number, date of birth, hire date, team, job role, and total experience.
Team: Defines teams within the organization (e.g., Software Development, Human Resources, Marketing).
Job: Stores job titles and their descriptions (e.g., Software Engineer, HR Manager, Marketing Specialist).
