CREATE TABLE EMPLOYEE_LEAVE_DOC
(
    EMP_LEAVE_DOC_ID                INTEGER NOT NULL PRIMARY KEY,
    EMPLOYEE_ID                     INTEGER NOT NULL,
    DOCUMENT_DATE                   CHAR(19) NOT NULL,
    LEAVE_YEAR                      INTEGER NOT NULL,
    LEAVE_MONTH                     INTEGER NOT NULL,
    LATE                            NUMERIC(12, 2),
    SICK_LEAVE                      NUMERIC(12, 2),
    PERSONAL_LEAVE                  NUMERIC(12, 2),
    EXTRA_LEAVE                     NUMERIC(12, 2),
    ANNUAL_LEAVE                    NUMERIC(12, 2),
    ABNORMAL_LEAVE                  NUMERIC(12, 2),
    DEDUCTION_LEAVE                 NUMERIC(12, 2),

    CREATE_DATE                     CHAR(19) NOT NULL,
    MODIFY_DATE                     CHAR(19) NOT NULL,

    FOREIGN KEY(EMPLOYEE_ID) REFERENCES EMPLOYEE(EMPLOYEE_ID)
);

CREATE SEQUENCE EMPLOYEE_LEAVE_DOC_SEQ START 1;

CREATE TABLE EMPLOYEE_LEAVE_RECORD
(
    EMP_LEAVE_REC_ID                INTEGER NOT NULL PRIMARY KEY,
    EMP_LEAVE_DOC_ID                INTEGER NOT NULL,
    LEAVE_DATE                      CHAR(19) NOT NULL,
    LATE                            NUMERIC(12, 2),
    SICK_LEAVE                      NUMERIC(12, 2),
    PERSONAL_LEAVE                  NUMERIC(12, 2),
    EXTRA_LEAVE                     NUMERIC(12, 2),
    ANNUAL_LEAVE                    NUMERIC(12, 2),
    ABNORMAL_LEAVE                  NUMERIC(12, 2),
    DEDUCTION_LEAVE                 NUMERIC(12, 2),

    CREATE_DATE                     CHAR(19) NOT NULL,
    MODIFY_DATE                     CHAR(19) NOT NULL,

    FOREIGN KEY(EMP_LEAVE_DOC_ID) REFERENCES EMPLOYEE_LEAVE_DOC(EMP_LEAVE_DOC_ID)
);

CREATE SEQUENCE EMPLOYEE_LEAVE_RECORD_SEQ START 1;

