namespace DATA.Repository.Abstraction
{
    public enum HistoricFetchMode
    {
        AtExactTime,
        AllTime,
        ActiveBetween,
        ActiveWithin,
        ActiveThrough,
        Invalid
    }

    public enum RequestStatus
    {
        NEW,
        SUCCEEDED,
        SUCCEEDED_WITH_ERRORS,
        SUCCEEDED_BUT_EMPTY,
        FAILED,
    }

}
