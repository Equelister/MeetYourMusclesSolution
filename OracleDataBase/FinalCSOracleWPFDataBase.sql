--------------------------------------------------------
--  File created - Monday-July-06-2020   
--------------------------------------------------------
DROP SEQUENCE "MEET_TABLE_SEQ";
DROP SEQUENCE "MEET_TABLE_SEQ1";
DROP SEQUENCE "PLACE_TABLE_SEQ";
DROP SEQUENCE "PLACE_TABLE_SEQ1";
DROP SEQUENCE "TRAINER_PASSWORD_TABLE_SEQ";
DROP SEQUENCE "TRAINER_PASSWORD_TABLE_SEQ1";
DROP SEQUENCE "TRAINER_PASSWORD_TABLE_SEQ2";
DROP SEQUENCE "TRAINER_TABLE_SEQ";
DROP SEQUENCE "TRAINER_TABLE_SEQ1";
DROP SEQUENCE "TRAINER_TABLE_SEQ2";
DROP SEQUENCE "TRAINER_TABLE_SEQ3";
DROP SEQUENCE "USER_PASSWORD_TABLE_SEQ";
DROP SEQUENCE "USER_PASSWORD_TABLE_SEQ1";
DROP SEQUENCE "USER_PASSWORD_TABLE_SEQ2";
DROP SEQUENCE "USER_TABLE_SEQ";
DROP SEQUENCE "USER_TABLE_SEQ1";
DROP SEQUENCE "USER_TABLE_SEQ2";
DROP TABLE "MEET_TABLE" cascade constraints;
DROP TABLE "PLACE_TABLE" cascade constraints;
DROP TABLE "TRAINER_PASSWORD_TABLE" cascade constraints;
DROP TABLE "TRAINER_TABLE" cascade constraints;
DROP TABLE "USER_PASSWORD_TABLE" cascade constraints;
DROP TABLE "USER_TABLE" cascade constraints;
--------------------------------------------------------
--  DDL for Sequence MEET_TABLE_SEQ
--------------------------------------------------------

   CREATE SEQUENCE  "MEET_TABLE_SEQ"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 21 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence MEET_TABLE_SEQ1
--------------------------------------------------------

   CREATE SEQUENCE  "MEET_TABLE_SEQ1"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 21 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence PLACE_TABLE_SEQ
--------------------------------------------------------

   CREATE SEQUENCE  "PLACE_TABLE_SEQ"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 21 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence PLACE_TABLE_SEQ1
--------------------------------------------------------

   CREATE SEQUENCE  "PLACE_TABLE_SEQ1"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 21 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence TRAINER_PASSWORD_TABLE_SEQ
--------------------------------------------------------

   CREATE SEQUENCE  "TRAINER_PASSWORD_TABLE_SEQ"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 21 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence TRAINER_PASSWORD_TABLE_SEQ1
--------------------------------------------------------

   CREATE SEQUENCE  "TRAINER_PASSWORD_TABLE_SEQ1"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence TRAINER_PASSWORD_TABLE_SEQ2
--------------------------------------------------------

   CREATE SEQUENCE  "TRAINER_PASSWORD_TABLE_SEQ2"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 21 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence TRAINER_TABLE_SEQ
--------------------------------------------------------

   CREATE SEQUENCE  "TRAINER_TABLE_SEQ"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 21 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence TRAINER_TABLE_SEQ1
--------------------------------------------------------

   CREATE SEQUENCE  "TRAINER_TABLE_SEQ1"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence TRAINER_TABLE_SEQ2
--------------------------------------------------------

   CREATE SEQUENCE  "TRAINER_TABLE_SEQ2"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence TRAINER_TABLE_SEQ3
--------------------------------------------------------

   CREATE SEQUENCE  "TRAINER_TABLE_SEQ3"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 21 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence USER_PASSWORD_TABLE_SEQ
--------------------------------------------------------

   CREATE SEQUENCE  "USER_PASSWORD_TABLE_SEQ"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 21 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence USER_PASSWORD_TABLE_SEQ1
--------------------------------------------------------

   CREATE SEQUENCE  "USER_PASSWORD_TABLE_SEQ1"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence USER_PASSWORD_TABLE_SEQ2
--------------------------------------------------------

   CREATE SEQUENCE  "USER_PASSWORD_TABLE_SEQ2"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 21 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence USER_TABLE_SEQ
--------------------------------------------------------

   CREATE SEQUENCE  "USER_TABLE_SEQ"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 21 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence USER_TABLE_SEQ1
--------------------------------------------------------

   CREATE SEQUENCE  "USER_TABLE_SEQ1"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 21 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence USER_TABLE_SEQ2
--------------------------------------------------------

   CREATE SEQUENCE  "USER_TABLE_SEQ2"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Table MEET_TABLE
--------------------------------------------------------

  CREATE TABLE "MEET_TABLE" 
   (	"ID" NUMBER(*,0), 
	"DATE_AND_TIME" DATE, 
	"DURATION" NUMBER(*,0), 
	"ACCEPTED" NUMBER DEFAULT '0', 
	"ISNEW" NUMBER DEFAULT '1', 
	"USER_TABLE_ID" NUMBER(*,0), 
	"TRAINER_TABLE_ID" NUMBER(*,0), 
	"PLACE_TABLE_ID" NUMBER(*,0)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table PLACE_TABLE
--------------------------------------------------------

  CREATE TABLE "PLACE_TABLE" 
   (	"ID" NUMBER(*,0), 
	"CITY" NVARCHAR2(50), 
	"POSTCODE" NVARCHAR2(10), 
	"STREET" NVARCHAR2(50), 
	"DESCRIPTION" NVARCHAR2(100), 
	"TRAINER_TABLE_ID" NUMBER(*,0)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table TRAINER_PASSWORD_TABLE
--------------------------------------------------------

  CREATE TABLE "TRAINER_PASSWORD_TABLE" 
   (	"ID" NUMBER(*,0), 
	"PASSWORD" NVARCHAR2(50), 
	"TRAINER_TABLE_ID" NUMBER(*,0), 
	"EMAIL" NVARCHAR2(50)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table TRAINER_TABLE
--------------------------------------------------------

  CREATE TABLE "TRAINER_TABLE" 
   (	"ID" NUMBER(*,0), 
	"FIRST_NAME" NVARCHAR2(50), 
	"LAST_NAME" NVARCHAR2(50), 
	"EMAIL" NVARCHAR2(50), 
	"PHONE_NUMBER" NVARCHAR2(20), 
	"COST_PER_HOUR" NUMBER(*,0), 
	"IMAGE" BLOB
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" 
 LOB ("IMAGE") STORE AS BASICFILE (
  TABLESPACE "SYSTEM" ENABLE STORAGE IN ROW CHUNK 8192 RETENTION 
  NOCACHE LOGGING 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)) ;
--------------------------------------------------------
--  DDL for Table USER_PASSWORD_TABLE
--------------------------------------------------------

  CREATE TABLE "USER_PASSWORD_TABLE" 
   (	"ID" NUMBER(*,0), 
	"PASSWORD" NVARCHAR2(50), 
	"USER_TABLE_ID" NUMBER(*,0), 
	"EMAIL" NVARCHAR2(50)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table USER_TABLE
--------------------------------------------------------

  CREATE TABLE "USER_TABLE" 
   (	"ID" NUMBER(*,0), 
	"FIRST_NAME" NVARCHAR2(50), 
	"LAST_NAME" NVARCHAR2(50), 
	"EMAIL" NVARCHAR2(50), 
	"PHONE_NUMBER" NVARCHAR2(20), 
	"IMAGE" BLOB
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" 
 LOB ("IMAGE") STORE AS BASICFILE (
  TABLESPACE "SYSTEM" ENABLE STORAGE IN ROW CHUNK 8192 RETENTION 
  NOCACHE LOGGING 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)) ;
--------------------------------------------------------
--  DDL for Index USER_TABLE_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "USER_TABLE_PK" ON "USER_TABLE" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index MEET_TABLE_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "MEET_TABLE_PK" ON "MEET_TABLE" ("ID", "USER_TABLE_ID", "TRAINER_TABLE_ID", "PLACE_TABLE_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index USER_PASSWORD_TABLE__IDX
--------------------------------------------------------

  CREATE UNIQUE INDEX "USER_PASSWORD_TABLE__IDX" ON "USER_PASSWORD_TABLE" ("USER_TABLE_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index PLACE_TABLE_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "PLACE_TABLE_PK" ON "PLACE_TABLE" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index TRAINER_PASSWORD_TABLE_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "TRAINER_PASSWORD_TABLE_PK" ON "TRAINER_PASSWORD_TABLE" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index TRAINER_TABLE_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "TRAINER_TABLE_PK" ON "TRAINER_TABLE" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index TRAINER_PASSWORD_TABLE__IDX
--------------------------------------------------------

  CREATE UNIQUE INDEX "TRAINER_PASSWORD_TABLE__IDX" ON "TRAINER_PASSWORD_TABLE" ("TRAINER_TABLE_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index USER_PASSWORD_TABLE_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "USER_PASSWORD_TABLE_PK" ON "USER_PASSWORD_TABLE" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Trigger MEET_TABLE_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "MEET_TABLE_TRG" 
BEFORE INSERT ON MEET_TABLE 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    IF INSERTING AND :NEW.ID IS NULL THEN
      SELECT MEET_TABLE_SEQ1.NEXTVAL INTO :NEW.ID FROM SYS.DUAL;
    END IF;
  END COLUMN_SEQUENCES;
END;
/
ALTER TRIGGER "MEET_TABLE_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger PLACE_TABLE_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "PLACE_TABLE_TRG" 
BEFORE INSERT ON PLACE_TABLE 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    IF INSERTING AND :NEW.ID IS NULL THEN
      SELECT PLACE_TABLE_SEQ1.NEXTVAL INTO :NEW.ID FROM SYS.DUAL;
    END IF;
  END COLUMN_SEQUENCES;
END;
/
ALTER TRIGGER "PLACE_TABLE_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger TRAINER_PASSWORD_TABLE_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "TRAINER_PASSWORD_TABLE_TRG" 
BEFORE INSERT ON TRAINER_PASSWORD_TABLE 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    IF INSERTING AND :NEW.ID IS NULL THEN
      SELECT TRAINER_PASSWORD_TABLE_SEQ2.NEXTVAL INTO :NEW.ID FROM SYS.DUAL;
    END IF;
  END COLUMN_SEQUENCES;
END;
/
ALTER TRIGGER "TRAINER_PASSWORD_TABLE_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger TRAINER_TABLE_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "TRAINER_TABLE_TRG" 
BEFORE INSERT ON TRAINER_TABLE 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    IF INSERTING AND :NEW.ID IS NULL THEN
      SELECT TRAINER_TABLE_SEQ3.NEXTVAL INTO :NEW.ID FROM SYS.DUAL;
    END IF;
  END COLUMN_SEQUENCES;
END;
/
ALTER TRIGGER "TRAINER_TABLE_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger USER_PASSWORD_TABLE_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "USER_PASSWORD_TABLE_TRG" 
BEFORE INSERT ON USER_PASSWORD_TABLE 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    IF INSERTING AND :NEW.ID IS NULL THEN
      SELECT USER_PASSWORD_TABLE_SEQ2.NEXTVAL INTO :NEW.ID FROM SYS.DUAL;
    END IF;
  END COLUMN_SEQUENCES;
END;
/
ALTER TRIGGER "USER_PASSWORD_TABLE_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger USER_TABLE_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "USER_TABLE_TRG" 
BEFORE INSERT ON USER_TABLE 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    IF INSERTING AND :NEW.ID IS NULL THEN
      SELECT USER_TABLE_SEQ2.NEXTVAL INTO :NEW.ID FROM SYS.DUAL;
    END IF;
  END COLUMN_SEQUENCES;
END;
/
ALTER TRIGGER "USER_TABLE_TRG" ENABLE;
--------------------------------------------------------
--  Constraints for Table MEET_TABLE
--------------------------------------------------------

  ALTER TABLE "MEET_TABLE" ADD CONSTRAINT "MEET_TABLE_PK" PRIMARY KEY ("ID", "USER_TABLE_ID", "TRAINER_TABLE_ID", "PLACE_TABLE_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "MEET_TABLE" MODIFY ("PLACE_TABLE_ID" NOT NULL ENABLE);
  ALTER TABLE "MEET_TABLE" MODIFY ("TRAINER_TABLE_ID" NOT NULL ENABLE);
  ALTER TABLE "MEET_TABLE" MODIFY ("USER_TABLE_ID" NOT NULL ENABLE);
  ALTER TABLE "MEET_TABLE" MODIFY ("ISNEW" NOT NULL ENABLE);
  ALTER TABLE "MEET_TABLE" MODIFY ("ACCEPTED" NOT NULL ENABLE);
  ALTER TABLE "MEET_TABLE" MODIFY ("DURATION" NOT NULL ENABLE);
  ALTER TABLE "MEET_TABLE" MODIFY ("DATE_AND_TIME" NOT NULL ENABLE);
  ALTER TABLE "MEET_TABLE" MODIFY ("ID" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table USER_PASSWORD_TABLE
--------------------------------------------------------

  ALTER TABLE "USER_PASSWORD_TABLE" MODIFY ("EMAIL" NOT NULL ENABLE);
  ALTER TABLE "USER_PASSWORD_TABLE" ADD CONSTRAINT "USER_PASSWORD_TABLE_PK" PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "USER_PASSWORD_TABLE" MODIFY ("USER_TABLE_ID" NOT NULL ENABLE);
  ALTER TABLE "USER_PASSWORD_TABLE" MODIFY ("PASSWORD" NOT NULL ENABLE);
  ALTER TABLE "USER_PASSWORD_TABLE" MODIFY ("ID" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table TRAINER_PASSWORD_TABLE
--------------------------------------------------------

  ALTER TABLE "TRAINER_PASSWORD_TABLE" MODIFY ("EMAIL" NOT NULL ENABLE);
  ALTER TABLE "TRAINER_PASSWORD_TABLE" ADD CONSTRAINT "TRAINER_PASSWORD_TABLE_PK" PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "TRAINER_PASSWORD_TABLE" MODIFY ("TRAINER_TABLE_ID" NOT NULL ENABLE);
  ALTER TABLE "TRAINER_PASSWORD_TABLE" MODIFY ("PASSWORD" NOT NULL ENABLE);
  ALTER TABLE "TRAINER_PASSWORD_TABLE" MODIFY ("ID" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table PLACE_TABLE
--------------------------------------------------------

  ALTER TABLE "PLACE_TABLE" ADD CONSTRAINT "PLACE_TABLE_PK" PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "PLACE_TABLE" MODIFY ("TRAINER_TABLE_ID" NOT NULL ENABLE);
  ALTER TABLE "PLACE_TABLE" MODIFY ("STREET" NOT NULL ENABLE);
  ALTER TABLE "PLACE_TABLE" MODIFY ("POSTCODE" NOT NULL ENABLE);
  ALTER TABLE "PLACE_TABLE" MODIFY ("CITY" NOT NULL ENABLE);
  ALTER TABLE "PLACE_TABLE" MODIFY ("ID" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table TRAINER_TABLE
--------------------------------------------------------

  ALTER TABLE "TRAINER_TABLE" ADD CONSTRAINT "TRAINER_TABLE_PK" PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "TRAINER_TABLE" MODIFY ("PHONE_NUMBER" NOT NULL ENABLE);
  ALTER TABLE "TRAINER_TABLE" MODIFY ("EMAIL" NOT NULL ENABLE);
  ALTER TABLE "TRAINER_TABLE" MODIFY ("LAST_NAME" NOT NULL ENABLE);
  ALTER TABLE "TRAINER_TABLE" MODIFY ("FIRST_NAME" NOT NULL ENABLE);
  ALTER TABLE "TRAINER_TABLE" MODIFY ("ID" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table USER_TABLE
--------------------------------------------------------

  ALTER TABLE "USER_TABLE" ADD CONSTRAINT "USER_TABLE_PK" PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "USER_TABLE" MODIFY ("PHONE_NUMBER" NOT NULL ENABLE);
  ALTER TABLE "USER_TABLE" MODIFY ("EMAIL" NOT NULL ENABLE);
  ALTER TABLE "USER_TABLE" MODIFY ("LAST_NAME" NOT NULL ENABLE);
  ALTER TABLE "USER_TABLE" MODIFY ("FIRST_NAME" NOT NULL ENABLE);
  ALTER TABLE "USER_TABLE" MODIFY ("ID" NOT NULL ENABLE);
--------------------------------------------------------
--  Ref Constraints for Table MEET_TABLE
--------------------------------------------------------

  ALTER TABLE "MEET_TABLE" ADD CONSTRAINT "MEET_TABLE_PLACE_TABLE_FK" FOREIGN KEY ("PLACE_TABLE_ID")
	  REFERENCES "PLACE_TABLE" ("ID") ENABLE;
  ALTER TABLE "MEET_TABLE" ADD CONSTRAINT "MEET_TABLE_TRAINER_TABLE_FK" FOREIGN KEY ("TRAINER_TABLE_ID")
	  REFERENCES "TRAINER_TABLE" ("ID") ENABLE;
  ALTER TABLE "MEET_TABLE" ADD CONSTRAINT "MEET_TABLE_USER_TABLE_FK" FOREIGN KEY ("USER_TABLE_ID")
	  REFERENCES "USER_TABLE" ("ID") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table PLACE_TABLE
--------------------------------------------------------

  ALTER TABLE "PLACE_TABLE" ADD CONSTRAINT "PLACE_TABLE_TRAINER_TABLE_FK" FOREIGN KEY ("TRAINER_TABLE_ID")
	  REFERENCES "TRAINER_TABLE" ("ID") ENABLE;
