-- Database: DVP_Debt

-- DROP DATABASE IF EXISTS "DVP_Debt";

CREATE DATABASE "DVP_Debt"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Spanish_Colombia.1252'
    LC_CTYPE = 'Spanish_Colombia.1252'
    LOCALE_PROVIDER = 'libc'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;
------------------------------------------------------
-- Table: public.Users

-- DROP TABLE IF EXISTS public."Users";

CREATE TABLE IF NOT EXISTS public."Users"
(
    "Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "Name" character varying(100) COLLATE pg_catalog."default" NOT NULL,
    "Email" character varying(150) COLLATE pg_catalog."default",
    "Password" character varying(250) COLLATE pg_catalog."default",
    "Register_date" timestamp without time zone DEFAULT now(),
    CONSTRAINT users_pkey PRIMARY KEY ("Id"),
    CONSTRAINT users_email_key UNIQUE ("Email")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Users"
    OWNER to postgres;

-----------------------------------------------

-- Table: public.States

-- DROP TABLE IF EXISTS public."States";

CREATE TABLE IF NOT EXISTS public."States"
(
    "Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "Name" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT states_pkey PRIMARY KEY ("Id"),
    CONSTRAINT states_name_key UNIQUE ("Name")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."States"
    OWNER to postgres;

----------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS public."Debts"
(
    "Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "DebtorId" integer NOT NULL,
    "CreditorId" integer NOT NULL,
    "Amount" numeric(12,2) NOT NULL,
    "Description" text COLLATE pg_catalog."default",
    "Creation_date" timestamp without time zone DEFAULT now(),
    "StateId" integer NOT NULL DEFAULT 1,
    CONSTRAINT debts_pkey PRIMARY KEY ("Id"),
    CONSTRAINT debts_creditorid_fkey FOREIGN KEY ("CreditorId")
        REFERENCES public."Users" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT debts_debtorid_fkey FOREIGN KEY ("DebtorId")
        REFERENCES public."Users" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT debts_stateid_fkey FOREIGN KEY ("StateId")
        REFERENCES public."States" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Debts"
    OWNER to postgres;
-------------------------------------------------------------------------

-- Table: public.Payments

-- DROP TABLE IF EXISTS public."Payments";

CREATE TABLE IF NOT EXISTS public."Payments"
(
    "Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "DebtId" integer NOT NULL,
    "Payment_amount" numeric(12,2) NOT NULL,
    "Payment_date" timestamp without time zone DEFAULT now(),
    CONSTRAINT payments_pkey PRIMARY KEY ("Id"),
    CONSTRAINT payments_debtid_fkey FOREIGN KEY ("DebtId")
        REFERENCES public."Debts" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Payments"
    OWNER to postgres;

------------------------------------------------------------------