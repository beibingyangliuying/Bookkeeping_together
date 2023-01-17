# Entity

-   `Account`: Your accounts for use.
-   `InCategory`: Categories of income.
-   `OutCategory`: Categories of outcome.
-   `InRecord`: Record your income.
-   `OutRecord`: Record your outcome.
-   `TransferRecord`: Transfer record, which means transferring from one account to another.

---

# E-R Diagram

```mermaid
erDiagram
    Account {
        int row_id PK
        string name
    }
    InCategory{
        int row_id PK
        string name
    }
    OutCategory{
        int row_id PK
        string name
    }
    InRecord{
        int row_id PK
        string account FK "Refers Account.name"
        string category FK "Refers InCategory.name"
        datetime datetime
        double money
        string remark
    }
    OutRecord{
        int row_id PK 
        string account FK "Refers Account.name"
        string category FK "Refers OutCategory.name"
        datetime datetime
        double money
        string remark
    }
    TransferRecord{
        int row_id PK
        datetime datetime
        double money
        string out_account FK "Refers Account.name"
        string in_account FK "Refers Account.name"
        string remark
    }
    Account || --|{ InRecord : correspond
    Account || --|{ OutRecord : correspond
    Account || --|{ TransferRecord : correspond
    InCategory || --|{ InRecord : correspond
    OutCategory || --|{ OutRecord : correspond
    
```

---

# Constraints

-   `InRecord`:
    -   `money`: should be greater than 0.
-   `OutRecord`:
    -   `money`: should be greater than 0.
-   `TransferRecord`:
    -   `money`:  should be greater than 0.
    -   `out_account` can't equal with `in_account` (In a transfer record, inflow and outflow in the same account is meaningless).

---

