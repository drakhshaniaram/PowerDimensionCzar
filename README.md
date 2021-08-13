# PowerDimensionHandler
 A Dynamics AX toolkit which allows developers to work with dimensions very easily. 
 It consists of some methods that empower the X++ programmer to get whatever they want from dimensions.
 
 # Features
 1. Highly flexible: by implementing dynamic parameter technique
 2. Very powerful: by dynamically generating just enough SQL queries the in runtime
 3. Memory sufficient: by optimizing the queries
 4. Easy to use: as it's observable, without need to tackle complex tables and helper classes, just input what you have and get what you want 

# GetDefaultDimensionFromAttrs
Gets some dimension attributes and their values and returns the corresponding DefaultDimension.
```csharp
static void TEST_GetDefaultDimensionFromAttrs(Args _args)
{
    DimensionDefault    defaultDimension1, 
                        defaultDimension2, 
                        defaultDimension3, 
                        defaultDimension4;
    ;
    
    defaultDimension1 = PowerDimensionCzar::GetDefaultDimensionFromAttrs(["BusinessUnit", "BU-100000"]);
    defaultDimension2 = PowerDimensionCzar::GetDefaultDimensionFromAttrs(["BusinessUnit", "BU-100000", "CostCenter", "CC-100000"]);
    defaultDimension3 = PowerDimensionCzar::GetDefaultDimensionFromAttrs(["BusinessUnit", "BU-100000", "CostCenter", "CC-100000", "Vendor", "V-10000"]);
    defaultDimension4 = PowerDimensionCzar::GetDefaultDimensionFromAttrs(["BusinessUnit", "BU-100000", "CostCenter", "CC-100000", "Vendor", "V-10000", "BankAccount", "BA-10000"]);
    
    info(strFmt("%1, %2, %3, %4", defaultDimension1, defaultDimension2, defaultDimension3, defaultDimension4));
}
```
result will be like `5637146826`

# GetAllDimensionAttributeTitles
Retuns all the dimesnion attibutes that are defined in the system.
```csharp
static void TEST_GetAllDimensionAttributeTitles(Args _args)
{
    Set     input;
    Set     result1, result2;
    ;
    
    // Default usage
    result1 = PowerDimensionCzar::GetAllDimensionAttributeTitles();
    info(strFmt("%1", result1.toString()));
    
    // Excluding some attributes
    input = new Set(Types::String);
    input.add("BusinessUnit");
    input.add("CostCenter");
    result2 = PowerDimensionCzar::GetAllDimensionAttributeTitles(input);
    info(strFmt("%1", result2.toString()));
}
```
result1 will be like `{"BankAccount", "BusinessUnit", "CardNo", "CostCenter", "Customer", "ExpenseType", "FixedAsset", "MainAccount", "Vendor", "Worker"}`

result2 will be like `{"BankAccount", "CardNo", "Customer", "ExpenseType", "FixedAsset", "MainAccount", "Vendor", "Worker"}`

# GetLedgerDimensionFromOffsetAcc
Recieves a desired offset account and return the corresponding LedgerDimension.
```csharp
static void TEST_GetLedgerDimensionFromOffsetAccs(Args _args)
{
    DimensionDefault        ledgerDim;
    ;
    
    ledgerDim = PowerDimensionCzar::GetLedgerDimensionFromOffsetAcc(159173);
    info(strFmt("%1", ledgerDim));
}
```
ledgerDim will be like `5482546267`