---
title: Recording Actions
weight: 10
---

It is not always possible to split the code we want to record into a separate method.
For these cases it is also possible to record custom actions.

Use the `RecordAction` method on the API and provide it with an action to run.

```csharp
api.RecordAction("myAction", () => {
    // ...
});
```

The provided name is used as the method name for this action when it is printed with the results.
