/// <summary>
/// Created and maintained By Jalal Derakhshani
/// drakhshani.aram@hotmail.com
/// 2021
/// </summary>

class PowerDimensionCzar
{
}

public static str GenerateExcludingString(Set _values = null)
{
    str             result;
    container       baseCon;

    baseCon = conDel(_values.pack(), 1, 3);
    result = strReplace(con2Str(baseCon), ",", ",!");
    result  = "!" + result;

    return result;
}

public static Set GetAllDimensionAttributeTitles(Set _exclude = new Set(Types::String))
{
    DimensionAttribute dimensionAttribute;
    Set                result = new Set(Types::String) ;
    ;

    while
    select Name from dimensionAttribute group by dimensionAttribute.Name where !(dimensionAttribute.Name like "SystemGeneratedAttribute*")
    {
        if(!_exclude.in(dimensionAttribute.Name))
            result.add(dimensionAttribute.Name);
    }

    return result;
}

public static DimensionDefault GetDefaultDimensionFromAttrs(container _dimAttrs, boolean _excludeOtherAttrs = true)
{
    int                              currPair;
    Query                            q;
    QueryRun                         qr;
    QueryBuildDataSource             qbds, qbdsExclude;
    DefaultDimensionView             result;

    Set                              attrs = new Set(Types::String);
    int                              attrPairsCount = conLen(_dimAttrs) / 2;
    int                              attrIdx;

    q = new Query();


    for(currPair = 1; currPair <= attrPairsCount; currPair++)
    {
        switch(currPair)
        {
            case 1:
                qbds = q.addDataSource(tableNum(DefaultDimensionView));
                attrIdx = 1;
                break;
            default:
                qbds = qbds.addDataSource(tableNum(DefaultDimensionView));
                qbds.joinMode(JoinMode::InnerJoin);
                qbds.relations(false);
                qbds.addLink(fieldNum(DefaultDimensionView, DefaultDimension), fieldNum(DefaultDimensionView, DefaultDimension));
        }

        qbds.addRange(fieldNum(DefaultDimensionView, Name)).value(conPeek(_dimAttrs, attrIdx));
        qbds.addRange(fieldNum(DefaultDimensionView, DisplayValue)).value(conPeek(_dimAttrs, attrIdx + 1));

        attrs.add(conPeek(_dimAttrs, attrIdx));
        attrIdx += 2;



    }

    qbdsExclude = qbds.addDataSource(tableNum(DefaultDimensionView));
    qbdsExclude.joinMode(JoinMode::NoExistsJoin);
    qbdsExclude.relations(false);
    qbdsExclude.addLink(fieldNum(DefaultDimensionView, DefaultDimension), fieldNum(DefaultDimensionView, DefaultDimension));
    qbdsExclude.addRange(fieldNum(DefaultDimensionView, Name)).value(PowerDimensionCzar::GenerateExcludingString(attrs));

    qr = new QueryRun(q);
    while(qr.next())
    {
        result = qr.get(tableNum(DefaultDimensionView));
        if(result.DefaultDimension)
                return result.DefaultDimension;
    }
    return 0;
}

public static DimensionDefault GetLedgerDimensionFromOffsetAcc(int _offsetAccount)
{
    container dimensions;
    RecId dimRes;
    ;
    try
    {
        dimensions = [_offsetAccount, _offsetAccount, 0];

        dimRes = AxdDimensionUtil::getLedgerAccountId(dimensions);
    }
    catch
    {
        Global::exceptionTextFallThrough();
    }
    return dimRes;
}