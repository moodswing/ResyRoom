using System;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ResyRoom.Infraestructura.Extensiones
{
    public static class HtmlExt
    {
        public static MvcHtmlString RadioButtonForEnum<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var names = Enum.GetNames(metaData.ModelType);
            var sb = new StringBuilder();
            foreach (var name in names)
            {
                var id = string.Format(
                    "{0}_{1}_{2}",
                    htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix,
                    metaData.PropertyName,
                    name
                );

                var radio = htmlHelper.RadioButtonFor(expression, name, new { id }).ToHtmlString();

                sb.AppendFormat("{0} <label for=\"{1}\">{2}</label>", radio, id, HttpUtility.HtmlEncode(name));
            }
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString CustomNameTextArea(this HtmlHelper htmlHelper, string name, string metadataPropertyName)
        {
            var fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

            var tagBuilder = new TagBuilder("textarea");
            tagBuilder.GenerateId(fullName);
            tagBuilder.MergeAttribute("rows", "2", true);
            tagBuilder.MergeAttribute("cols", "20", true);
            tagBuilder.MergeAttribute("name", fullName, true);

            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(fullName, out modelState) && modelState.Errors.Count > 0)
                tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);

            tagBuilder.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(metadataPropertyName));

            var modelMetadata = ModelMetadata.FromStringExpression(metadataPropertyName, htmlHelper.ViewContext.ViewData);
            string value;
            if (modelState != null && modelState.Value != null)
                value = modelState.Value.AttemptedValue;
            else if (modelMetadata.Model != null)
                value = modelMetadata.Model.ToString();
            else
                value = String.Empty;
            tagBuilder.SetInnerText(Environment.NewLine + value);

            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        }

    }
}