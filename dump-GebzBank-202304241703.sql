--
-- PostgreSQL database dump
--

-- Dumped from database version 15.2
-- Dumped by pg_dump version 15.2

-- Started on 2023-04-24 17:03:22

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

DROP DATABASE "GebzBank";
--
-- TOC entry 3322 (class 1262 OID 16398)
-- Name: GebzBank; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "GebzBank" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_United States.1252';


ALTER DATABASE "GebzBank" OWNER TO postgres;

\connect "GebzBank"

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 4 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: pg_database_owner
--

CREATE SCHEMA public;


ALTER SCHEMA public OWNER TO pg_database_owner;

--
-- TOC entry 3323 (class 0 OID 0)
-- Dependencies: 4
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: pg_database_owner
--

COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 214 (class 1259 OID 16399)
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    email character varying(255) NOT NULL,
    "passwordHash" character varying(255) NOT NULL,
    salt character varying(255) NOT NULL,
    "accountNumber" integer NOT NULL,
    balance numeric NOT NULL,
    guid uuid NOT NULL
);


ALTER TABLE public.users OWNER TO postgres;

--
-- TOC entry 3316 (class 0 OID 16399)
-- Dependencies: 214
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3173 (class 2606 OID 16407)
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (guid);


-- Completed on 2023-04-24 17:03:23

--
-- PostgreSQL database dump complete
--

