
CREATE TABLE ACCOUNT_DOC_PAYMENT
(
    ACT_DOC_PAYMENT_ID      INTEGER NOT NULL PRIMARY KEY,
    ACCOUNT_DOC_ID          INTEGER NOT NULL,
    PAYMENT_TYPE            INTEGER NOT NULL, /* 1=cash, 2=bank xfer, 3=credit card */
    BANK_ID                 INTEGER,
    CASH_ACCOUNT_ID         INTEGER,
    PAID_AMOUNT             NUMERIC(10, 4),
    NOTE                    TEXT,

    CREATE_DATE CHAR(19)    NOT NULL,
    MODIFY_DATE CHAR(19)    NOT NULL,

    FOREIGN KEY(ACCOUNT_DOC_ID) REFERENCES ACCOUNT_DOC(ACCOUNT_DOC_ID),
    FOREIGN KEY(BANK_ID) REFERENCES MASTER_REF(MASTER_ID),
    FOREIGN KEY(CASH_ACCOUNT_ID) REFERENCES CASH_ACCOUNT(CASH_ACCOUNT_ID)
);

CREATE SEQUENCE ACCOUNT_DOC_PAYMENT_SEQ START 1;

INSERT INTO GLOBAL_VARIABLE
(GLOBAL_VARIABLE_ID, VARIABLE_NAME, VARIABLE_TYPE, VARIABLE_CATEGORY, VARIABLE_VALUE, VARIABLE_DESC, CREATE_DATE, MODIFY_DATE)
VALUES
(3, 'DOC_SEQ_LENGTH_DEFAULT', 1, 1, '5', 'Default sequence length of document number', '2017/11/27 00:00:00', '2017/11/27 00:00:00');

INSERT INTO GLOBAL_VARIABLE
(GLOBAL_VARIABLE_ID, VARIABLE_NAME, VARIABLE_TYPE, VARIABLE_CATEGORY, VARIABLE_VALUE, VARIABLE_DESC, CREATE_DATE, MODIFY_DATE)
VALUES
(4, 'DOC_NO_CASH_DEFAULT', 2, 1, 'cash-${year}-${seq}', '', '2017/11/27 00:00:00', '2017/11/27 00:00:00');

INSERT INTO GLOBAL_VARIABLE
(GLOBAL_VARIABLE_ID, VARIABLE_NAME, VARIABLE_TYPE, VARIABLE_CATEGORY, VARIABLE_VALUE, VARIABLE_DESC, CREATE_DATE, MODIFY_DATE)
VALUES
(5, 'DOC_NO_DEBT_DEFAULT', 2, 1, 'ar-${year}-${seq}', '', '2017/11/27 00:00:00', '2017/11/27 00:00:00');

INSERT INTO GLOBAL_VARIABLE
(GLOBAL_VARIABLE_ID, VARIABLE_NAME, VARIABLE_TYPE, VARIABLE_CATEGORY, VARIABLE_VALUE, VARIABLE_DESC, CREATE_DATE, MODIFY_DATE)
VALUES
(6, 'DOC_NO_CASH_DEFAULT_NV', 2, 1, '*cash-${year}-${seq}', '', '2017/11/27 00:00:00', '2017/11/27 00:00:00');

INSERT INTO GLOBAL_VARIABLE
(GLOBAL_VARIABLE_ID, VARIABLE_NAME, VARIABLE_TYPE, VARIABLE_CATEGORY, VARIABLE_VALUE, VARIABLE_DESC, CREATE_DATE, MODIFY_DATE)
VALUES
(7, 'DOC_NO_DEBT_DEFAULT_NV', 2, 1, '*ar-${year}-${seq}', '', '2017/11/27 00:00:00', '2017/11/27 00:00:00');

INSERT INTO GLOBAL_VARIABLE
(GLOBAL_VARIABLE_ID, VARIABLE_NAME, VARIABLE_TYPE, VARIABLE_CATEGORY, VARIABLE_VALUE, VARIABLE_DESC, CREATE_DATE, MODIFY_DATE)
VALUES
(8, 'DOC_NO_YEAR_OFFSET_DEFAULT', 1, 1, '0', 'Default document number year offset', '2017/11/27 00:00:00', '2017/11/27 00:00:00');

INSERT INTO GLOBAL_VARIABLE
(GLOBAL_VARIABLE_ID, VARIABLE_NAME, VARIABLE_TYPE, VARIABLE_CATEGORY, VARIABLE_VALUE, VARIABLE_DESC, CREATE_DATE, MODIFY_DATE)
VALUES
(9, 'DOC_NO_RESET_DEFAULT', 1, 1, '2', '1=Monthly, 2=Yearly', '2017/11/27 00:00:00', '2017/11/27 00:00:00');

ALTER TABLE ACCOUNT_DOC ADD COLUMN REF_DOCUMENT_NO TEXT;

ALTER TABLE LOCATION ADD COLUMN BRANCH_ID INTEGER;
ALTER TABLE LOCATION ADD FOREIGN KEY (BRANCH_ID) REFERENCES MASTER_REF(MASTER_ID);

ALTER TABLE ACCOUNT_DOC_PAYMENT ADD COLUMN DIRECTION INTEGER; /* 1=In, 2=Out (Change) */
UPDATE ACCOUNT_DOC_PAYMENT SET DIRECTION = 1;

INSERT INTO GLOBAL_VARIABLE
(GLOBAL_VARIABLE_ID, VARIABLE_NAME, VARIABLE_TYPE, VARIABLE_CATEGORY, VARIABLE_VALUE, VARIABLE_DESC, CREATE_DATE, MODIFY_DATE)
VALUES
(10, 'SALE_PETTY_CASH_ACCT_NO', 2, 1, 'PettyCash_Here', 'Put the petty default cash account number here', '2017/12/05 00:00:00', '2017/12/05 00:00:00');

ALTER TABLE ACCOUNT_DOC_PAYMENT ALTER COLUMN PAID_AMOUNT TYPE NUMERIC(10, 2);

CREATE TABLE INVENTORY_ADJUSTMENT
(
    INVENTORY_ADJ_ID        INTEGER NOT NULL PRIMARY KEY,
    DOC_ID                  INTEGER NOT NULL,
    ITEM_ID                 INTEGER NOT NULL,
    QUANTITY                NUMERIC(10, 4),
    AMOUNT                  NUMERIC(10, 4),

    CREATE_DATE CHAR(19)    NOT NULL,
    MODIFY_DATE CHAR(19)    NOT NULL,

    FOREIGN KEY(DOC_ID) REFERENCES INVENTORY_DOC(DOC_ID),
    FOREIGN KEY(ITEM_ID) REFERENCES ITEM(ITEM_ID)
);

CREATE SEQUENCE INVENTORY_ADJUSTMENT_SEQ START 1;

ALTER TABLE BRANCH_CONFIG ADD COLUMN VOID_BILL_PASSWORD TEXT;
ALTER TABLE INVENTORY_DOC ADD COLUMN ADJUSTMENT_BY INTEGER; /* 1=By Amount, 2=By Unit Price */

ALTER TABLE ACCOUNT_DOC ADD COLUMN ENTITY_ADDRESS_ID INTEGER;
ALTER TABLE ACCOUNT_DOC ADD FOREIGN KEY (ENTITY_ADDRESS_ID) REFERENCES ENTITY_ADDRESS(ENTITY_ADDRESS_ID);

UPDATE INVENTORY_DOC SET ADJUSTMENT_BY = 1;

CREATE SEQUENCE TEST_POST_PATCH_SEQ START 1;


CREATE TABLE REPORT_CONFIG
(
    REPORT_CONFIG_ID        INTEGER NOT NULL PRIMARY KEY,
    REPORT_NAME             TEXT NOT NULL UNIQUE,
    CONFIG_VALUE            TEXT,
    USER_ID                 INTEGER, /* Null if Global Level */

    CREATE_DATE CHAR(19)    NOT NULL,
    MODIFY_DATE CHAR(19)    NOT NULL
);

CREATE SEQUENCE REPORT_CONFIG_SEQ START 1;

DELETE FROM USERS WHERE PASSWORD = '1234';

ALTER TABLE ENTITY ADD COLUMN NAME_PREFIX INTEGER;
ALTER TABLE ENTITY ADD FOREIGN KEY (NAME_PREFIX) REFERENCES MASTER_REF(MASTER_ID);

ALTER TABLE USERS ADD COLUMN IS_INTIAL TEXT;
UPDATE USERS SET IS_INTIAL = 'N';

ALTER TABLE USERS ADD COLUMN EMAIL TEXT;

ALTER TABLE ITEM ADD VAT_FLAG CHAR(1);
UPDATE ITEM SET VAT_FLAG = 'Y';


CREATE TABLE REPORT_FILTER
(
    REPORT_FILTER_ID        INTEGER NOT NULL PRIMARY KEY,
    REPORT_NAME             TEXT NOT NULL,
    REPORT_GROUP            TEXT NOT NULL,
    REPORT_SEQ              INTEGER,
    IS_SELECTED             CHAR(1) NOT NULL,

    CREATE_DATE CHAR(19)    NOT NULL,
    MODIFY_DATE CHAR(19)    NOT NULL
);

CREATE SEQUENCE REPORT_FILTER_SEQ START 1;

ALTER TABLE REPORT_FILTER ADD REPORT_NS INTEGER;

/* For receipt */
ALTER TABLE ACCOUNT_DOC ADD COLUMN RECEIPT_PAID_AMT NUMERIC(12, 4);
ALTER TABLE ACCOUNT_DOC ADD COLUMN RECEIPT_DEBT_AMT NUMERIC(12, 4);

UPDATE ACCOUNT_DOC SET RECEIPT_PAID_AMT = 0.00;
UPDATE ACCOUNT_DOC SET RECEIPT_DEBT_AMT = 0.00;

/* For debt document, amount has been paid and remain */
ALTER TABLE ACCOUNT_DOC ADD COLUMN ARAP_REMAIN_AMT NUMERIC(12, 4);
ALTER TABLE ACCOUNT_DOC ADD COLUMN ARAP_PAID_AMT NUMERIC(12, 4);
ALTER TABLE ACCOUNT_DOC ADD COLUMN ARAP_PAID_OFF_FLAG CHAR(1);

UPDATE ACCOUNT_DOC SET ARAP_REMAIN_AMT = 0.00;
UPDATE ACCOUNT_DOC SET ARAP_PAID_AMT = 0.00;

CREATE TABLE RECEIPT_ITEM
(
    RECEIPT_ITEM_ID         INTEGER NOT NULL PRIMARY KEY,
    ACCOUNT_DOC_ID          INTEGER NOT NULL,
    DEBT_BEGIN_AMOUNT       NUMERIC(12, 4) NOT NULL,
    DEBT_END_AMOUNT         NUMERIC(12, 4) NOT NULL,
    PAID_AMOUNT             NUMERIC(12, 4) NOT NULL,
    DOCUMENT_ID             INTEGER NOT NULL,

    CREATE_DATE CHAR(19)    NOT NULL,
    MODIFY_DATE CHAR(19)    NOT NULL
);

CREATE SEQUENCE RECEIPT_ITEM_SEQ START 1;

ALTER TABLE RECEIPT_ITEM ADD FOREIGN KEY (ACCOUNT_DOC_ID) REFERENCES ACCOUNT_DOC(ACCOUNT_DOC_ID);
ALTER TABLE RECEIPT_ITEM ADD FOREIGN KEY (DOCUMENT_ID) REFERENCES ACCOUNT_DOC(ACCOUNT_DOC_ID);
