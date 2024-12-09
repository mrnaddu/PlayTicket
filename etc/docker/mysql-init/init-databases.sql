-- Create databases if they do not exist
CREATE DATABASE IF NOT EXISTS db_office;
CREATE DATABASE IF NOT EXISTS db_compliance;

-- Grant all privileges on the databases to the root user
GRANT ALL PRIVILEGES ON db_office.* TO 'root'@'%';
GRANT ALL PRIVILEGES ON db_compliance.* TO 'root'@'%';

-- Flush privileges to ensure that the changes take effect
FLUSH PRIVILEGES;