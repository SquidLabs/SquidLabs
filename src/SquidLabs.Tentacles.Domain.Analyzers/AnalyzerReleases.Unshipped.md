### New Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|----------------
SL001   | Usage    | Error    | ValueObjectRecordTypeEnforcementAnalyzer, Enforces that all implementations of `IValueObject<TKey>` must be record types.
SL002   | Usage    | Error    | ValueObjectPropertiesEnforcementAnalyzer, Enforces that all properties in implementations of `IValueObject<TKey>` are value types, such as primitives, DateTime, string, etc.