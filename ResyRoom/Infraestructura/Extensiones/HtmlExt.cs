using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ResyRoom.Infraestructura.Extensiones
{
    public static class HtmlExt
    {
        public static MvcHtmlString DescriptionFor<TModel, TValue>(this HtmlHelper<TModel> self, Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, self.ViewData);
            var description = metadata.Description;

            return MvcHtmlString.Create(string.Format(@"<span class='field-info-description'>{0}</span>", description));
        }

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

        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var enumType = GetNonNullableModelType(metadata);
            var values = Enum.GetValues(enumType).Cast<TEnum>();

            var items = from value in values
                        select new SelectListItem
                        {
                            Text = GetEnumDescription(value),
                            Value = value.ToString(),
                            Selected = value.Equals(metadata.Model)
                        };

            // If the enum is nullable, add an 'empty' item to the collection

            return htmlHelper.DropDownListFor(expression, items, htmlAttributes);
        }

        private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
        {
            var realModelType = modelMetadata.ModelType;
            var underlyingType = Nullable.GetUnderlyingType(realModelType);

            if (underlyingType != null)
                realModelType = underlyingType;

            return realModelType;
        }

        private static string GetEnumDescription<TEnum>(TEnum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
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