CREATE TABLE COST_DOCUMENT
(
    COST_DOC_ID                     INTEGER NOT NULL PRIMARY KEY,
    COST_YEAR                       INTEGER NOT NULL,
    DOCUMENT_STATUS                 INTEGER NOT NULL,
    BEGIN_STOCK_BALANCE             NUMERIC(12, 2),
    END_STOCK_BALANCE               NUMERIC(12, 2),
    IN_AMOUNT                       NUMERIC(12, 2),
    OUT_AMOUNT                      NUMERIC(12, 2),

    CREATE_DATE                     CHAR(19) NOT NULL,
    MODIFY_DATE                     CHAR(19) NOT NULL
);

CREATE SEQUENCE COST_DOCUMENT_SEQ START 1;

CREATE TABLE COST_DOCUMENT_ITEM
(
    COST_DOC_ITEM_ID                INTEGER NOT NULL PRIMARY KEY,
    COST_DOC_ID                     INTEGER NOT NULL,
    ITEM_TYPE                       INTEGER NOT NULL,
    JAN_AMOUNT                      NUMERIC(12, 2),
    FEB_AMOUNT                      NUMERIC(12, 2),
    MAR_AMOUNT                      NUMERIC(12, 2),
    APR_AMOUNT                      NUMERIC(12, 2),
    MAY_AMOUNT                      NUMERIC(12, 2),
    JUN_AMOUNT                      NUMERIC(12, 2),
    JUL_AMOUNT                      NUMERIC(12, 2),
    AUG_AMOUNT                      NUMERIC(12, 2),
    SEP_AMOUNT                      NUMERIC(12, 2),
    OCT_AMOUNT                      NUMERIC(12, 2),
    NOV_AMOUNT                      NUMERIC(12, 2),
    DEC_AMOUNT                      NUMERIC(12, 2),
    TOT_AMOUNT                      NUMERIC(12, 2),

    CREATE_DATE                     CHAR(19) NOT NULL,
    MODIFY_DATE                     CHAR(19) NOT NULL,

    FOREIGN KEY(COST_DOC_ID) REFERENCES COST_DOCUMENT(COST_DOC_ID)
);

CREATE SEQUENCE COST_DOCUMENT_ITEM_SEQ START 1;

ALTER TABLE EMPLOYEE ADD SALARY NUMERIC(12, 2);


ALTER TABLE PAYROLL_DEDUCTION_ITEM ADD DURATION NUMERIC(12, 2);
ALTER TABLE PAYROLL_DEDUCTION_ITEM ADD DURATION_MINUTE NUMERIC(12, 2);
ALTER TABLE PAYROLL_DEDUCTION_ITEM ADD DURATION_UNIT TEXT;
ALTER TABLE PAYROLL_DEDUCTION_ITEM ADD DURATION_HINT TEXT;

ALTER TABLE OT_DOCUMENT ADD DEDUCTION_MINUTE_TOTAL NUMERIC(12, 2);
ALTER TABLE OT_DOCUMENT ADD DEDUCTION_AMOUNT NUMERIC(12, 2);

ALTER TABLE EMPLOYEE ADD RESIGNED_FLAG CHAR(1);

UPDATE EMPLOYEE SET RESIGNED_FLAG = 'N';

ALTER TABLE OT_DOCUMENT ADD DEDUCTION_HOUR_ROUNDED_TOTAL NUMERIC(12, 2);

ALTER TABLE PAYROLL_DEDUCTION_ITEM ADD SLIP_RECEIVE_OT NUMERIC(12, 2);

ALTER TABLE PAYROLL_DEDUCTION_ITEM DROP COLUMN SLIP_RECEIVE_OT;

ALTER TABLE PAYROLL_DOC_ITEM ADD SLIP_RECEIVE_OT NUMERIC(12, 2);

ALTER TABLE OT_DOCUMENT ADD WORKED_AMOUNT_TOTAL NUMERIC(12, 2);

UPDATE EMPLOYEE SET RESIGNED_FLAG = 'N' WHERE RESIGNED_FLAG IS NULL;

UPDATE EMPLOYEE SET RESIGNED_FLAG = 'N' WHERE RESIGNED_FLAG != 'Y';

ALTER TABLE ENTITY ADD PROMPT_PAY_ID TEXT;

ALTER TABLE OT_DOC_ITEM ADD RECEIVE_TYPE CHAR(1);
UPDATE OT_DOC_ITEM SET RECEIVE_TYPE = '0';

UPDATE INVENTORY_TX SET RETURNED_QUANTITY = 0.00;
UPDATE INVENTORY_TX SET RETURNED_QUANTITY_NEED = ITEM_QUANTITY;

UPDATE ITEM SET BORROW_FLAG = 'Y';
