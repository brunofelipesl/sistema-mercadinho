SELECT 'CREATE DATABASE mercadinho'
WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = 'mercadinho')
\gexec

SELECT 'CREATE DATABASE mercadinho_auth'
WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = 'mercadinho_auth')
\gexec

\connect mercadinho_auth;

CREATE TABLE IF NOT EXISTS users (
    id SERIAL PRIMARY KEY,
    username VARCHAR(255) NOT NULL UNIQUE,
    password_hash TEXT NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE
);

INSERT INTO users (username, password_hash, is_active)
VALUES ('admin', '8C6976E5B5410415BDE908BD4DEE15DFB16D6C7FBA37B1FBCB1A73AA3E4ACDE6', TRUE)
ON CONFLICT (username) DO NOTHING;
