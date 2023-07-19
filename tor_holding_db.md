# tor-holding-db
CREATE DATABASE tor_holding
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Turkish_Turkey.1254'
    LC_CTYPE = 'Turkish_Turkey.1254'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;
		
DROP TABLE IF EXISTS "public"."t_user";
CREATE TABLE "public"."t_user" (
  "id" uuid NOT NULL,
  "adi" text COLLATE "pg_catalog"."default",
  "soyadi" text COLLATE "pg_catalog"."default",
  "cep" text COLLATE "pg_catalog"."default",
  "adres" text COLLATE "pg_catalog"."default",
  "eposta" text COLLATE "pg_catalog"."default",
  "sifre" text COLLATE "pg_catalog"."default"
)
;


DROP TABLE IF EXISTS "public"."t_user_login_log";
CREATE TABLE "public"."t_user_login_log" (
  "id" uuid NOT NULL,
  "kullanici_id" uuid,
  "giris_tarihi" timestamp(6),
  "cikis_tarihi" timestamp(6)
)
;


ALTER TABLE "public"."t_user" ADD CONSTRAINT "user_pkey" PRIMARY KEY ("id");


ALTER TABLE "public"."t_user_login_log" ADD CONSTRAINT "user_login_log_pkey" PRIMARY KEY ("id");
