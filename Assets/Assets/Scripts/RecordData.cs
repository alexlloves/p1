using System;
using System.Collections;
using System.Collections.Generic;

public class RecordData
{
    List<int> records;

    public RecordData(List<int> records)
    {
        this.records = records;
    }

    public List<int> GetTotalRecords()
    {
        return this.records;
    }

    public void SetRecords(List<int> records)
    {
        this.records = records;
    }
}
