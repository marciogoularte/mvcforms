<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Documentation Examples</title>
    
    <style>pre{background:#fafafa;padding:1em;border:dashed 1px blue;</style>
</head>
<body>
    <div>
        <h1>Documentation Examples</h1>
        
        <p>View overview at <a href="http://mvcforms.codeplex.com/wikipage?title=Working with forms">Codeplex</a>.</p>
        
        <dl>
            <dt><%=Html.ActionLink("Simple template", "Simple") %></dt>
            <dd>
                <pre><span style="background-color: Yellow;">&lt;%</span><span style="color: Blue;">@</span> <span style="color: rgb(163, 21, 21);">Page</span> <span style="color: Red;">Title</span><span style="color: Blue;">=</span><span style="color: Blue;">""</span> <span style="color: Red;">Language</span><span style="color: Blue;">=</span><span style="color: Blue;">"C#"</span> <span style="color: Red;">Inherits</span><span style="color: Blue;">=</span><span style="color: Blue;">"System.Web.Mvc.ViewPage&lt;JL.Web.Forms.BaseForm&gt;"</span> <span style="background-color: Yellow;">%&gt;</span>

<span style="color: Blue;">&lt;</span><span style="color: rgb(163, 21, 21);">form</span> <span style="color: Red;">action</span><span style="color: Blue;">=</span><span style="color: Blue;">"" method="post"</span><span style="color: Blue;">&gt;</span>
    <span style="background-color: Yellow;">&lt;%=</span>Model.AsP <span style="background-color: Yellow;">%&gt;</span>
    <span style="color: Blue;">&lt;</span><span style="color: rgb(163, 21, 21);">input</span> <span style="color: Red;">type</span><span style="color: Blue;">=</span><span style="color: Blue;">"submit"</span> <span style="color: Red;">value</span><span style="color: Blue;">=</span><span style="color: Blue;">"Submit"</span> <span style="color: Blue;">/&gt;</span>
<span style="color: Blue;">&lt;/</span><span style="color: rgb(163, 21, 21);">form</span><span style="color: Blue;">&gt;</span>
</pre>
            </dd>
            
            <dt><%=Html.ActionLink("Custom layout template", "CustomLayout") %></dt>
            <dd>
                <pre><span style="background-color: Yellow;">&lt;%</span><span style="color: Blue;">@</span> <span style="color: rgb(163, 21, 21);">Page</span> <span style="color: Red;">Title</span><span style="color: Blue;">=</span><span style="color: Blue;">""</span> <span style="color: Red;">Language</span><span style="color: Blue;">=</span><span style="color: Blue;">"C#"</span> <span style="color: Red;">Inherits</span><span style="color: Blue;">=</span><span style="color: Blue;">"System.Web.Mvc.ViewPage&lt;JL.Web.Forms.Form&gt;"</span> <span style="background-color: Yellow;">%&gt;</span>

<span style="color: Blue;">&lt;</span><span style="color: rgb(163, 21, 21);">form</span> <span style="color: Red;">action</span><span style="color: Blue;">=</span><span style="color: Blue;">"" method="post"</span><span style="color: Blue;">&gt;</span>
    <span style="color: Blue;">&lt;</span><span style="color: rgb(163, 21, 21);">div</span> <span style="color: Red;">class</span><span style="color: Blue;">=</span><span style="color: Blue;">"fieldWrapper"</span><span style="color: Blue;">&gt;</span>
        <span style="background-color: Yellow;">&lt;%=</span>Model[<span style="color: rgb(163, 21, 21);">"Subject"</span>].Errors <span style="background-color: Yellow;">%&gt;</span>
        <span style="color: Blue;">&lt;</span><span style="color: rgb(163, 21, 21);">label</span> <span style="color: Red;">for</span><span style="color: Blue;">=</span><span style="color: Blue;">"id_Subject"</span><span style="color: Blue;">&gt;</span>E-mail subject:<span style="color: Blue;">&lt;/</span><span style="color: rgb(163, 21, 21);">label</span><span style="color: Blue;">&gt;</span>
        <span style="background-color: Yellow;">&lt;%=</span>Model[<span style="color: rgb(163, 21, 21);">"Subject"</span>] <span style="background-color: Yellow;">%&gt;</span>
    <span style="color: Blue;">&lt;/</span><span style="color: rgb(163, 21, 21);">div</span><span style="color: Blue;">&gt;</span>
    <span style="color: Blue;">&lt;</span><span style="color: rgb(163, 21, 21);">div</span> <span style="color: Red;">class</span><span style="color: Blue;">=</span><span style="color: Blue;">"fieldWrapper"</span><span style="color: Blue;">&gt;</span>
        <span style="background-color: Yellow;">&lt;%=</span>Model[<span style="color: rgb(163, 21, 21);">"Message"</span>].Errors <span style="background-color: Yellow;">%&gt;</span>
        <span style="color: Blue;">&lt;</span><span style="color: rgb(163, 21, 21);">label</span> <span style="color: Red;">for</span><span style="color: Blue;">=</span><span style="color: Blue;">"id_Message"</span><span style="color: Blue;">&gt;</span>Your message:<span style="color: Blue;">&lt;/</span><span style="color: rgb(163, 21, 21);">label</span><span style="color: Blue;">&gt;</span>
        <span style="background-color: Yellow;">&lt;%=</span>Model[<span style="color: rgb(163, 21, 21);">"Message"</span>]<span style="background-color: Yellow;">%&gt;</span>
    <span style="color: Blue;">&lt;/</span><span style="color: rgb(163, 21, 21);">div</span><span style="color: Blue;">&gt;</span>
    <span style="color: Blue;">&lt;</span><span style="color: rgb(163, 21, 21);">div</span> <span style="color: Red;">class</span><span style="color: Blue;">=</span><span style="color: Blue;">"fieldWrapper"</span><span style="color: Blue;">&gt;</span>
        <span style="background-color: Yellow;">&lt;%=</span>Model[<span style="color: rgb(163, 21, 21);">"Sender"</span>].Errors <span style="background-color: Yellow;">%&gt;</span>
        <span style="color: Blue;">&lt;</span><span style="color: rgb(163, 21, 21);">label</span> <span style="color: Red;">for</span><span style="color: Blue;">=</span><span style="color: Blue;">"id_Sender"</span><span style="color: Blue;">&gt;</span>Your email address:<span style="color: Blue;">&lt;/</span><span style="color: rgb(163, 21, 21);">label</span><span style="color: Blue;">&gt;</span>
        <span style="background-color: Yellow;">&lt;%=</span>Model[<span style="color: rgb(163, 21, 21);">"Sender"</span>]<span style="background-color: Yellow;">%&gt;</span>
    <span style="color: Blue;">&lt;/</span><span style="color: rgb(163, 21, 21);">div</span><span style="color: Blue;">&gt;</span>
    <span style="color: Blue;">&lt;</span><span style="color: rgb(163, 21, 21);">div</span> <span style="color: Red;">class</span><span style="color: Blue;">=</span><span style="color: Blue;">"fieldWrapper"</span><span style="color: Blue;">&gt;</span>
        <span style="background-color: Yellow;">&lt;%=</span>Model[<span style="color: rgb(163, 21, 21);">"CCMyself"</span>].Errors<span style="background-color: Yellow;">%&gt;</span>
        <span style="color: Blue;">&lt;</span><span style="color: rgb(163, 21, 21);">label</span> <span style="color: Red;">for</span><span style="color: Blue;">=</span><span style="color: Blue;">"id_CCMyself"</span><span style="color: Blue;">&gt;</span>CC yourself?<span style="color: Blue;">&lt;/</span><span style="color: rgb(163, 21, 21);">label</span><span style="color: Blue;">&gt;</span>
        <span style="background-color: Yellow;">&lt;%=</span>Model[<span style="color: rgb(163, 21, 21);">"CCMyself"</span>]<span style="background-color: Yellow;">%&gt;</span>
    <span style="color: Blue;">&lt;/</span><span style="color: rgb(163, 21, 21);">div</span><span style="color: Blue;">&gt;</span>
    <span style="color: Blue;">&lt;</span><span style="color: rgb(163, 21, 21);">input</span> <span style="color: Red;">type</span><span style="color: Blue;">=</span><span style="color: Blue;">"submit"</span> <span style="color: Red;">value</span><span style="color: Blue;">=</span><span style="color: Blue;">"Send message"</span> <span style="color: Blue;">/&gt;</span>
<span style="color: Blue;">&lt;/</span><span style="color: rgb(163, 21, 21);">form</span><span style="color: Blue;">&gt;</span>
</pre>
            </dd>
            
            <dt><%=Html.ActionLink("Custom error layout template", "CustomErrorLayout") %></dt>
            <dd>
                <pre><span style="background-color: Yellow;">&lt;%</span> <span style="color: Blue;">if</span> (Model[<span style="color: rgb(163, 21, 21);">"Subject"</span>].HasErrors) { <span style="background-color: Yellow;">%&gt;</span>
    <span style="color: Blue;">&lt;</span><span style="color: rgb(163, 21, 21);">ol</span><span style="color: Blue;">&gt;</span>
        <span style="background-color: Yellow;">&lt;%</span> <span style="color: Blue;">foreach</span> (<span style="color: Blue;">var</span> error <span style="color: Blue;">in</span> Model[<span style="color: rgb(163, 21, 21);">"Subject"</span>].Errors) { <span style="background-color: Yellow;">%&gt;</span>
            <span style="color: Blue;">&lt;</span><span style="color: rgb(163, 21, 21);">li</span><span style="color: Blue;">&gt;</span><span style="color: Blue;">&lt;</span><span style="color: rgb(163, 21, 21);">strong</span><span style="color: Blue;">&gt;</span><span style="background-color: Yellow;">&lt;%=</span> error <span style="background-color: Yellow;">%&gt;</span><span style="color: Blue;">&lt;/</span><span style="color: rgb(163, 21, 21);">strong</span><span style="color: Blue;">&gt;</span><span style="color: Blue;">&lt;/</span><span style="color: rgb(163, 21, 21);">li</span><span style="color: Blue;">&gt;</span>
        <span style="background-color: Yellow;">&lt;%</span>} <span style="background-color: Yellow;">%&gt;</span>
    <span style="color: Blue;">&lt;/</span><span style="color: rgb(163, 21, 21);">ol</span><span style="color: Blue;">&gt;</span>
<span style="background-color: Yellow;">&lt;%</span>} <span style="background-color: Yellow;">%&gt;</span>
</pre>
            </dd>
            
            <dt><%=Html.ActionLink("Looping over fields template", "LoopFields")%></dt>
            <dd>
                <pre><span style="color: Blue;">&lt;</span><span style="color: rgb(163, 21, 21);">form</span> <span style="color: Red;">action</span><span style="color: Blue;">=</span><span style="color: Blue;">"" method="post"</span><span style="color: Blue;">&gt;</span>
    <span style="background-color: Yellow;">&lt;%</span> <span style="color: Blue;">foreach</span> (<span style="color: Blue;">var</span> field <span style="color: Blue;">in</span> Model) { <span style="background-color: Yellow;">%&gt;</span>
        <span style="color: Blue;">&lt;</span><span style="color: rgb(163, 21, 21);">div</span> <span style="color: Red;">class</span><span style="color: Blue;">=</span><span style="color: Blue;">"fieldWrapper"</span><span style="color: Blue;">&gt;</span>
            <span style="background-color: Yellow;">&lt;%=</span>field.Errors <span style="background-color: Yellow;">%&gt;</span>
            <span style="background-color: Yellow;">&lt;%=</span>field.Label <span style="background-color: Yellow;">%&gt;</span>: <span style="background-color: Yellow;">&lt;%=</span> field <span style="background-color: Yellow;">%&gt;</span>
        <span style="color: Blue;">&lt;/</span><span style="color: rgb(163, 21, 21);">div</span><span style="color: Blue;">&gt;</span>
    <span style="background-color: Yellow;">&lt;%</span> } <span style="background-color: Yellow;">%&gt;</span>
    <span style="color: Blue;">&lt;</span><span style="color: rgb(163, 21, 21);">p</span><span style="color: Blue;">&gt;</span><span style="color: Blue;">&lt;</span><span style="color: rgb(163, 21, 21);">input</span> <span style="color: Red;">type</span><span style="color: Blue;">=</span><span style="color: Blue;">"submit"</span> <span style="color: Red;">value</span><span style="color: Blue;">=</span><span style="color: Blue;">"Send message"</span> <span style="color: Blue;">/&gt;</span><span style="color: Blue;">&lt;/</span><span style="color: rgb(163, 21, 21);">p</span><span style="color: Blue;">&gt;</span>
<span style="color: Blue;">&lt;/</span><span style="color: rgb(163, 21, 21);">form</span><span style="color: Blue;">&gt;</span>
</pre>
            </dd>
            
            <dt><%=Html.ActionLink("Reusable snippet template", "Reusable")%></dt>
            <dd>
                <pre><span style="color: Blue;">&lt;</span><span style="color: rgb(163, 21, 21);">form</span> <span style="color: Red;">action</span><span style="color: Blue;">=</span><span style="color: Blue;">"" method="post"</span><span style="color: Blue;">&gt;</span>
    <span style="background-color: Yellow;">&lt;%</span> Html.RenderPartial(<span style="color: rgb(163, 21, 21);">"FormSnippet"</span>, Model); <span style="background-color: Yellow;">%&gt;</span>
    <span style="color: Blue;">&lt;</span><span style="color: rgb(163, 21, 21);">p</span><span style="color: Blue;">&gt;</span><span style="color: Blue;">&lt;</span><span style="color: rgb(163, 21, 21);">input</span> <span style="color: Red;">type</span><span style="color: Blue;">=</span><span style="color: Blue;">"submit"</span> <span style="color: Red;">value</span><span style="color: Blue;">=</span><span style="color: Blue;">"Send message"</span> <span style="color: Blue;">/&gt;</span><span style="color: Blue;">&lt;/</span><span style="color: rgb(163, 21, 21);">p</span><span style="color: Blue;">&gt;</span>
<span style="color: Blue;">&lt;/</span><span style="color: rgb(163, 21, 21);">form</span><span style="color: Blue;">&gt;</span>

# In FormSnippet.ascx:

<span style="background-color: Yellow;">&lt;%</span> <span style="color: Blue;">foreach</span> (<span style="color: Blue;">var</span> field <span style="color: Blue;">in</span> Model) { <span style="background-color: Yellow;">%&gt;</span>
    <span style="color: Blue;">&lt;</span><span style="color: rgb(163, 21, 21);">div</span> <span style="color: Red;">class</span><span style="color: Blue;">=</span><span style="color: Blue;">"fieldWrapper"</span><span style="color: Blue;">&gt;</span>
        <span style="background-color: Yellow;">&lt;%=</span>field.Errors <span style="background-color: Yellow;">%&gt;</span>
        <span style="background-color: Yellow;">&lt;%=</span>field.LabelTag <span style="background-color: Yellow;">%&gt;</span>: <span style="background-color: Yellow;">&lt;%=</span> field <span style="background-color: Yellow;">%&gt;</span>
    <span style="color: Blue;">&lt;/</span><span style="color: rgb(163, 21, 21);">div</span><span style="color: Blue;">&gt;</span>
<span style="background-color: Yellow;">&lt;%</span> } <span style="background-color: Yellow;">%&gt;</span>
</pre>
            </dd>
        </dl>
    </div>
</body>
</html>
