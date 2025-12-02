IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'SystemTrackerDB')
BEGIN
    CREATE DATABASE SystemTrackerDB;
END

-- Use the newly created or existing database
USE SystemTrackerDB;

-- Create Users table if it doesn't exist
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Users')
BEGIN
    CREATE TABLE Users (
        user_id INT PRIMARY KEY IDENTITY,
        username VARCHAR(50) NOT NULL UNIQUE,
		hashed_password VARCHAR(255) NOT NULL,
        access_level VARCHAR(20) NOT NULL
    );
END

-- Create Devices table if it doesn't exist
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Devices')
BEGIN
    CREATE TABLE Devices (
        device_id INT PRIMARY KEY IDENTITY,
        processor VARCHAR(100),
        ram_size INT,
        os_version VARCHAR(50)
    );
END

-- Create Sessions table if it doesn't exist
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Sessions')
BEGIN
    CREATE TABLE Sessions (
        session_id INT PRIMARY KEY IDENTITY,
        session_timestamp DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
        runtime INT NOT NULL,
        CONSTRAINT fk_sessions_user FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE SET NULL ON UPDATE CASCADE,
        CONSTRAINT fk_sessions_device FOREIGN KEY (device_id) REFERENCES Devices(device_id) ON DELETE SET NULL ON UPDATE CASCADE
    );
END

-- Create Files table if it doesn't exist
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Files')
BEGIN
    CREATE TABLE Files (
        file_id INT PRIMARY KEY IDENTITY,
        file_path VARCHAR(255) NOT NULL,
        modified_date DATETIME NOT NULL,
        size INT NOT NULL,
        CONSTRAINT fk_files_session FOREIGN KEY (session_id) REFERENCES Sessions(session_id) ON DELETE SET NULL ON UPDATE CASCADE
    );
END