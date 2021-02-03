---
title: Recording Actions
weight: 10
---

It is not always possible to split the code we want to record into a separate method.
For these cases it is also possible to record custom actions.

Use the `RecordAction` method on the API and provide it with an action to run.

{{< highlight csharp "linenos=table" >}}
api.RecordAction("myAction", () => {
    // ...
});
{{< /highlight >}}

The provided name is used as the method name for this action when it is printed with the results.

### Example

The following code:

{{< highlight csharp "linenos=table" >}}
api.RecordAction("firstAction", () => {
    api.RecordAction("nestedAction", () => {
        // ...
    });
});
{{< /highlight >}}

Will generate the following nested result string:

{{< highlight text "linenos=table" >}}
+- 
   +- Action.firstAction          count:  1  sum: 1.008  avg: 1.008  max: 1.008  min: 1.008
      +- Action.nestedAction      count:  1  sum: 0.953  avg: 0.953  max: 0.953  min: 0.953
{{< /highlight >}}
