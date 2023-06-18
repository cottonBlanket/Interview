select MeterId, ArchiveType, ValueType, max(ValueTs) as ValueTs from MeterData
group by MeterId, ArchiveType, ValueType;