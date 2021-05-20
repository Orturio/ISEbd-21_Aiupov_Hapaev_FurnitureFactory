﻿using FurnitureFactoryBusinessLogics.HelperModels;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;

namespace FurnitureFactoryBusinessLogics.BusinessLogics
{
    static class SaveToWord
    {
        public static void CreateDocPurchase(WordInfo info)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(info.FileName, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                mainPart.Document = new Document();

                Body docBody = mainPart.Document.AppendChild(new Body());

                docBody.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> { (info.Title, new
WordTextProperties {Bold = true, Size = "24", } ) },
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));

                foreach (var purchase in info.Purchases)
                {
                    decimal sumToPayment = new decimal();
                    if (purchase.PurchaseSumToPayment == null)
                    {
                        sumToPayment = purchase.PurchaseSum;
                    }
                    else
                    {
                        sumToPayment = purchase.PurchaseSumToPayment.Value;
                    }

                    docBody.AppendChild(CreateParagraph(new WordParagraph
                    {                       
                        Texts = new List<(string, WordTextProperties)> 
                        {
                            ("Название: " + purchase.PurchaseName, new WordTextProperties {Bold = true, Size = "24", }),
                            (" || Сумма покупки: " + purchase.PurchaseSum.ToString(), new WordTextProperties {Bold = false, Size = "24", }),
                             (" || Сумма покупки к оплате: " + sumToPayment.ToString(), new WordTextProperties {Bold = false, Size = "24", })
                        },
                        TextProperties = new WordTextProperties
                        {
                            Size = "24",
                            JustificationValues = JustificationValues.Both
                        }
                    })); ;
                    foreach (var furniture in purchase.PurchaseFurniture)
                    {
                        docBody.AppendChild(CreateParagraph(new WordParagraph
                        {
                            Texts = new List<(string, WordTextProperties)>
                            {
                                ("Название мебели: " + furniture.Value.Item1, new WordTextProperties {Bold = false, Size = "24", }),
                                (" ||  Количество: " + furniture.Value.Item2.ToString(), new WordTextProperties {Bold = false, Size = "24", }),
                                (" ||  Сумма: " + furniture.Value.Item4.ToString(), new WordTextProperties {Bold = false, Size = "24", })
                            },
                            
                            TextProperties = new WordTextProperties
                            {
                                Size = "24",
                                JustificationValues = JustificationValues.Both
                            }
                        }));
                    }
                }
                
                docBody.AppendChild(CreateSectionProperties());

                wordDocument.MainDocumentPart.Document.Save();
            }
        }

        public static void CreateDocFurniture(WordInfo info)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(info.FileName, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                mainPart.Document = new Document();

                Body docBody = mainPart.Document.AppendChild(new Body());

                docBody.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties {Bold = true, Size = "24", } ) },
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));

                foreach (var furniture in info.Furnitures)
                {
                    docBody.AppendChild(CreateParagraph(new WordParagraph
                    {
                        Texts = new List<(string, WordTextProperties)> {("Название: " + furniture.FurnitureName, new WordTextProperties {Bold = true, Size = "24", }), 
                            (" Материал: " + furniture.Material.ToString() + " Цена: " + furniture.FurniturePrice, new WordTextProperties {Bold = false, Size = "24", }) },
                        TextProperties = new WordTextProperties
                        {
                            Size = "24",
                            JustificationValues = JustificationValues.Both
                        }
                    })); ;
                    foreach (var purchase in furniture.Purchases)
                    {
                        docBody.AppendChild(CreateParagraph(new WordParagraph
                        {
                            Texts = new List<(string, WordTextProperties)>
                            {
                                ("Название покупки: " + purchase.Item1, new WordTextProperties {Bold = false, Size = "24", }),
                                (" ||  Количество: " +  purchase.Item2.ToString(), new WordTextProperties {Bold = false, Size = "24", })                            
                            },

                            TextProperties = new WordTextProperties
                            {
                                Size = "24",
                                JustificationValues = JustificationValues.Both
                            }
                        }));
                    }
                }
                docBody.AppendChild(CreateSectionProperties());

                wordDocument.MainDocumentPart.Document.Save();
            }
        }

        /// <summary>
        /// Настройки страницы
        /// </summary>
        /// <returns></returns>
        private static SectionProperties CreateSectionProperties()
        {
            SectionProperties properties = new SectionProperties();

            PageSize pageSize = new PageSize
            {
                Orient = PageOrientationValues.Portrait
            };

            properties.AppendChild(pageSize);

            return properties;
        }

        /// <summary>
        /// Создание абзаца с текстом
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        private static Paragraph CreateParagraph(WordParagraph paragraph)
        {
            if (paragraph != null)
            {
                Paragraph docParagraph = new Paragraph();

                docParagraph.AppendChild(CreateParagraphProperties(paragraph.TextProperties));

                foreach (var run in paragraph.Texts)
                {
                    Run docRun = new Run();

                    RunProperties properties = new RunProperties();
                    properties.AppendChild(new FontSize { Val = run.Item2.Size });
                    if (run.Item2.Bold)
                    {
                        properties.AppendChild(new Bold());
                    }
                    docRun.AppendChild(properties);

                    docRun.AppendChild(new Text { Text = run.Item1, Space = SpaceProcessingModeValues.Preserve });

                    docParagraph.AppendChild(docRun);
                }

                return docParagraph;
            }
            return null;
        }

        /// <summary>
        /// Задание форматирования для абзаца
        /// </summary>
        /// <param name="paragraphProperties"></param>
        /// <returns></returns>
        private static ParagraphProperties CreateParagraphProperties(WordTextProperties paragraphProperties)
        {
            if (paragraphProperties != null)
            {
                ParagraphProperties properties = new ParagraphProperties();

                properties.AppendChild(new Justification()
                {
                    Val = paragraphProperties.JustificationValues
                });

                properties.AppendChild(new SpacingBetweenLines
                {
                    LineRule = LineSpacingRuleValues.Auto
                });

                properties.AppendChild(new Indentation());
                ParagraphMarkRunProperties paragraphMarkRunProperties = new ParagraphMarkRunProperties();

                if (!string.IsNullOrEmpty(paragraphProperties.Size))
                {
                    paragraphMarkRunProperties.AppendChild(new FontSize
                    {
                        Val = paragraphProperties.Size
                    });
                }
                properties.AppendChild(paragraphMarkRunProperties);

                return properties;
            }
            return null;
        }
    }
}
