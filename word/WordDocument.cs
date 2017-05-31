using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;

namespace word
{
    /// <summary>
    /// word文档操作类
    /// </summary>
    public class WordDocument
    {
        private DateTime beforTime;
        private DateTime afterTime;

        private Word.ApplicationClass wordApp;

        /// <summary>
        /// 获取word程序对象
        /// </summary>
        public Word.ApplicationClass WordApp
        {
            get { return wordApp; }
            set { wordApp = value; }
        }

        private Word.DocumentClass wordDoc;

        /// <summary>
        /// 返回文档对象
        /// </summary>
        public Word.DocumentClass WordDoc
        {
            get { return wordDoc; }
            set { wordDoc = value; }
        }

        private string fileName;

        /// <summary>
        /// 要保存的文件名称
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        /// <summary>
        /// 获取文档段落
        /// </summary>
        public Paragraphs Paragraphs
        {
            get { return this.WordDoc.Paragraphs; }
        }

        /// <summary>
        /// 根据文件名称创建一个文档。如果文件存在，将删除旧文件内容
        /// </summary>
        /// <param name="docFile"></param>
        public WordDocument(string docFile)
        {
            if (File.Exists(docFile))
            {
                File.Delete(docFile);
            }
            docFile = Path.GetFullPath(docFile);
            this.FileName = docFile;
            byte[] a = new byte[] {}; //自 己 加 的
            File.WriteAllBytes(docFile, a); //写入空文件
            object file = docFile;
            object missing = Missing.Value;
            this.beforTime = DateTime.Now; //开始时间
            this.WordApp = new ApplicationClass();
            this.afterTime = DateTime.Now; //结束时间
            this.WordDoc = (DocumentClass) WordApp.Documents.Open(ref file, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing);
        }

        /// <summary>
        /// 从模版文件创建文档
        /// </summary>
        /// <param name="savedocFile">要创建文档的保村路径</param>
        /// <param name="TemplateDoc">模板文件路径</param>
        public WordDocument(string savedocFile, string TemplateDoc)
        {
            savedocFile = Path.GetFullPath(savedocFile);
            this.FileName = savedocFile;
            TemplateDoc = Path.GetFullPath(TemplateDoc);

            object file = TemplateDoc; //模板文件

            object missing = Missing.Value;
            this.beforTime = DateTime.Now; //开始时间
            this.WordApp = new ApplicationClass();
            this.afterTime = DateTime.Now; //结束时间
            this.WordDoc = (DocumentClass) WordApp.Documents.Open(ref file, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing);
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        public void Save()
        {
            object format = WdSaveFormat.wdFormatDocument; //保存格式
            object miss = System.Reflection.Missing.Value;
            object fileName = this.FileName.Trim();

            wordDoc.SaveAs(ref fileName, ref format, ref miss,
                ref miss, ref miss, ref miss, ref miss,
                ref miss, ref miss, ref miss, ref miss,
                ref miss, ref miss, ref miss, ref miss,
                ref miss);

            object SaveChanges = WdSaveOptions.wdSaveChanges;
            object OriginalFormat = WdOriginalFormat.wdOriginalDocumentFormat;
            object RouteDocument = false;

            //wordDoc.Close(ref SaveChanges, ref OriginalFormat, ref RouteDocument);
            //wordApp.Quit(ref SaveChanges, ref OriginalFormat, ref RouteDocument);
        }

        /// <summary>
        /// 获取表格
        /// </summary>
        public Tables Tables
        {
            get { return this.WordDoc.Tables; }
        }

        /// <summary>
        /// 添加一行并设置值
        /// </summary>
        /// <param name="table">要操作的表格对象</param>
        /// <param name="values">值</param>
        public void AddRowWithValue(Table table, string[] values)
        {
            object row = table.Rows[table.Rows.Count];
            table.Rows.Add(ref row);
            Row row_ = table.Rows.Last;
            for (int i = 0; i < values.Length; i++)
            {
                table.Rows.Last.Cells[i + 1].Range.Text = values[i];
            }
        }

        /// <summary>
        /// 保存文件到指定的位置
        /// </summary>
        /// <param name="file">要保存的位置</param>
        public void SaveAs(string file)
        {
            object format = WdSaveFormat.wdFormatDocument; //保存格式
            object miss = System.Reflection.Missing.Value;
            object fileName = file;
            wordDoc.SaveAs(ref fileName, ref format, ref miss,
                ref miss, ref miss, ref miss, ref miss,
                ref miss, ref miss, ref miss, ref miss,
                ref miss, ref miss, ref miss, ref miss,
                ref miss);

            object SaveChanges = WdSaveOptions.wdSaveChanges;
            object OriginalFormat = WdOriginalFormat.wdOriginalDocumentFormat;
            object RouteDocument = false;

            wordDoc.Close(ref SaveChanges, ref OriginalFormat, ref RouteDocument);
            wordApp.Quit(ref SaveChanges, ref OriginalFormat, ref RouteDocument);
            //if (file.ToLower().Trim() != this.FileName.ToLower().Trim())
            //{
            //    File.Delete(this.FileName.Trim());
            //}
        }


        /// <summary>
        /// 在书签处插入值
        /// </summary>
        /// <param name="bookmark">书签</param>
        /// <param name="value">要插入的值</param>
        /// <returns>成功返回真，失败返回假</returns>
        public bool InsertValue(string bookmark, string value)
        {
            object bkObj = bookmark;
            if (wordApp.ActiveDocument.Bookmarks.Exists(bookmark))
            {
                wordApp.ActiveDocument.Bookmarks.get_Item(ref bkObj).Select();
                wordApp.Selection.TypeText(value);
                // wordApp.Selection.Range.Font.Size = 14;//标记下
                return true;
            }
            return false;
        }

        /// <summary>
        ///  在书签插处入表格,
        /// </summary>
        /// <param name="bookmark">bookmark书签</param>
        /// <param name="rows">行数</param>
        /// <param name="columns">列数</param>
        /// <param name="width">宽度</param>
        /// <returns>返回表格对象</returns>
        public Table InsertTable(string bookmark, int rows, int columns, float width)
        {
            object miss = System.Reflection.Missing.Value;
            object oStart = bookmark;
            Range range = wordDoc.Bookmarks.get_Item(ref oStart).Range; //表格插入位置
            Table newTable = wordDoc.Tables.Add(range, rows, columns, ref miss, ref miss);
            //设置表的格式
            newTable.Borders.Enable = 1; //允许有边框，默认没有边框(为0时报错，1为实线边框，2、3为虚线边框，以后的数字没试过)
            newTable.Borders.OutsideLineWidth = WdLineWidth.wdLineWidth050pt; //边框宽度
            if (width != 0)
            {
                newTable.PreferredWidth = width; //表格宽度
            }
            newTable.AllowPageBreaks = false;
            return newTable;
        }

        /// <summary>
        /// 在指定位置插入表格
        /// </summary>
        /// <param name="range">表格位置</param>
        /// <param name="rows">行数</param>
        /// <param name="columns">列数</param>
        /// <param name="width">宽度</param>
        /// <returns>返回表格对象</returns>
        public Table InsertTable(Range range, int rows, int columns, float width)
        {
            object miss = System.Reflection.Missing.Value;


            Table newTable = wordDoc.Tables.Add(range, rows, columns, ref miss, ref miss);
            //设置表的格式
            newTable.Borders.Enable = 1; //允许有边框，默认没有边框(为0时报错，1为实线边框，2、3为虚线边框，以后的数字没试过)
            newTable.Borders.OutsideLineWidth = WdLineWidth.wdLineWidth050pt; //边框宽度
            if (width != 0)
            {
                newTable.PreferredWidth = width; //表格宽度
            }
            newTable.AllowPageBreaks = false;
            return newTable;
        }

        public Table InsertTable(Range range, int rows, int columns)
        {
            object miss = System.Reflection.Missing.Value;


            Table newTable = wordDoc.Tables.Add(range, rows, columns, ref miss, ref miss);
            //设置表的格式
            newTable.Borders.Enable = 1; //允许有边框，默认没有边框(为0时报错，1为实线边框，2、3为虚线边框，以后的数字没试过)
            newTable.Borders.OutsideLineWidth = WdLineWidth.wdLineWidth050pt; //边框宽度

            newTable.AllowPageBreaks = true;
            return newTable;
        }


        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="table">表格对象</param>
        /// <param name="row1">开始行号</param>
        /// <param name="column1">开始列号</param>
        /// <param name="row2">结束行号</param>
        /// <param name="column2">结束列号</param>
        public void MergeCell(Microsoft.Office.Interop.Word.Table table, int row1, int column1, int row2, int column2)
        {
            table.Cell(row1, column1).Merge(table.Cell(row2, column2));
        }

        /// <summary>
        /// 设置段落对齐方式
        /// </summary>
        /// <param name="paragraph">段落</param>
        /// <param name="alignment">对齐方式</param>
        public void SetAlignment(Paragraph paragraph, Word_Alignment alignment)
        {
            paragraph.Alignment = (WdParagraphAlignment) alignment;
        }

        public void SetFont_Range()
        {
        }

        /// <summary>
        /// 设置表格的单元格的对齐方式
        /// </summary>
        /// <param name="table">表格对象索引</param>
        /// <param name="rowIndex">行号</param>
        /// <param name="clumnIndex">列号</param>
        /// <param name="align">水平对齐方式</param>
        /// <param name="valign">垂直对齐方式</param>
        public void SetTableCellAlignment(int table, int rowIndex, int clumnIndex, Word_Alignment align,
            Word_Alignment valign)
        {
            this.WordApp.ActiveDocument.Tables[table].Cell(rowIndex, clumnIndex).Select();
            this.WordApp.ActiveDocument.Tables[table].Cell(rowIndex, clumnIndex).VerticalAlignment =
                (WdCellVerticalAlignment) valign;
            this.WordApp.Selection.ParagraphFormat.Alignment = (WdParagraphAlignment) align;
        }

        /// <summary>
        /// 设置表格字体
        /// </summary>
        /// <param name="table">表格对象</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="size">字体大小</param>
        public void SetFont_Table(Microsoft.Office.Interop.Word.Table table, string fontName, double size)
        {
            if (size != 0)
            {
                table.Range.Font.Size = Convert.ToSingle(size);
            }
            if (fontName != "")
            {
                table.Range.Font.Name = fontName;
            }
        }

        /// <summary>
        /// 设置段落字体
        /// </summary>
        /// <param name="ph"></param>
        /// <param name="fontName"></param>
        /// <param name="size"></param>
        public void SetFont_Paragraph(Paragraph ph, string fontName, double size)
        {
            if (size != 0)
            {
                ph.Range.Font.Size = Convert.ToSingle(size);
            }
            if (fontName != "")
            {
                ph.Range.Font.Name = fontName;
            }
        }

        /// <summary>
        /// 设置一个范围的字体
        /// </summary>
        /// <param name="range">范围对象</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="size">大小</param>
        public void SetFont_Range(Range range, string fontName, double size)
        {
            if (size != 0)
            {
                range.Font.Size = Convert.ToSingle(size);
            }
            if (fontName != "")
            {
                range.Font.Name = fontName;
            }
        }

        /// <summary>
        /// 设置加粗
        /// </summary>
        /// <param name="range">设置的对象</param>
        /// <param name="Bold">是否加粗</param>
        public void setFontStyle_Range(Range range, bool Bold)
        {
            if (Bold)
            {
                range.Font.Bold = 1;
            }
            else
            {
                range.Font.Bold = 0;
            }
        }

        /// <summary>
        /// 设置字符间距
        /// </summary>
        /// <param name="range"></param>
        /// <param name="Spacing"></param>
        public void SetSpacing(Range range, float Spacing)
        {
            range.Font.Spacing = Spacing;
        }


        /// <summary>
        /// 设置表格是否具有边框
        /// </summary>
        /// <param name="table">表格对象</param>
        /// <param name="use">是否有边框</param>
        public void UseBorder_Table(Table table, bool use)
        {
            if (use)
            {
                table.Borders.Enable = 1; //允许有边框，默认没有边框(为0时报错，1为实线边框，2、3为虚线边框，以后的数字没试过)
            }
            else
            {
                table.Borders.Enable = 2; //允许有边框，默认没有边框(为0时报错，1为实线边框，2、3为虚线边框，以后的数字没试过)
            }
        }


        /// <summary>
        /// 给一个表格插入一行
        /// </summary>
        /// <param name="tb">表格对象</param>
        public void Table_AddRow(Table tb)
        {
            object miss = System.Reflection.Missing.Value;
            tb.Rows.Add(ref miss);
        }


        /// <summary>
        /// 给表格插入rows行
        /// </summary>
        /// <param name="table">表格对象</param>
        /// <param name="rows">行数</param>
        public void AddRow(Table table, int rows)
        {
            object miss = System.Reflection.Missing.Value;

            for (int i = 0; i < rows; i++)
            {
                table.Rows.Add(ref miss);
            }
        }

        /// <summary>
        /// 在文档末尾加入一行文本
        /// </summary>
        /// <param name="text"></param>
        public void AppendText(string text)
        {
            object oEndOfDoc = "\\endofdoc"; //WORD中预定义的书签
            object oMissing = System.Reflection.Missing.Value;
            Range wrdRng = this.WordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range; //获取当前文档的末尾位置。 
            wrdRng.InsertAfter(text);
        }

        /// <summary>
        /// 在文档末尾插入一段新的段落
        /// </summary>
        public void InsertParagraph()
        {
            object oEndOfDoc = "\\endofdoc"; //WORD中预定义的书签
            object oMissing = System.Reflection.Missing.Value;
            Range wrdRng = this.WordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range; //获取当前文档的末尾位置。 
            wrdRng.InsertParagraphAfter();
        }

        /// <summary>
        /// 在指定的范围前加入一段话
        /// </summary>
        /// <param name="range"></param>
        /// <param name="text"></param>
        public void InsertBefore(Range range, string text)
        {
            range.InsertBefore(text);
        }

        /// <summary>
        /// 获取文档结束位置
        /// </summary>
        /// <returns></returns>
        public Range GetEndRange()
        {
            object oEndOfDoc = "\\endofdoc"; //WORD中预定义的书签
            object oMissing = System.Reflection.Missing.Value;
            Range wrdRng = this.WordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range; //获取当前文档的末尾位置。 
            return wrdRng;
        }

        /// <summary>
        /// 获取文档开始位置
        /// </summary>
        /// <returns></returns>
        public Range GetStartRange()
        {
            object oEndOfDoc = "\\StartOfDoc"; //WORD中预定义的书签
            object oMissing = System.Reflection.Missing.Value;
            Range wrdRng = this.WordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range; //获取当前文档的末尾位置。 
            return wrdRng;
        }

        /// <summary>
        /// 设置单元格内容
        /// </summary>
        /// <param name="table">表格对象</param>
        /// <param name="row">行数</param>
        /// <param name="Column">列数</param>
        /// <param name="value">值</param>
        public void SetCellValue(Table table, int row, int Column, string value)
        {
            table.Cell(row, Column).Range.Text = value;
        }

        /// <summary>
        /// 插入图片
        /// </summary>
        /// <param name="range">图片位置</param>
        /// <param name="picPath">图片文件位置</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        public InlineShape InsertPicture(Range range, string picPath, float width, float height)
        {
            object link = false;
            object savewith = true;
            object rng = range;
            InlineShape inline = this.WordDoc.InlineShapes.AddPicture(Path.GetFullPath(picPath), ref link, ref savewith,
                ref rng);

            inline.Width = width;
            inline.Height = height;
            return inline;
        }

        /// <summary>
        /// 关闭进程
        /// </summary>
        public void Close()
        {
            try
            {
                object miss = Missing.Value;
                //this.WordApp.Quit(ref miss, ref miss, ref miss);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(this.WordApp.ActiveDocument);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(this.WordApp);

                System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("WINWORD");
                if (process != null)
                {
                    for (int i = 0; i < process.Length; i++)
                    {
                        if (process[i].StartTime <= this.afterTime && process[i].StartTime >= this.beforTime)
                        {
                            process[i].Kill();
                            System.Diagnostics.Debug.Write("ok");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// 查找替换，只替换查找到的第一个
        /// </summary>
        /// <param name="strOldText">要查找的字符串</param>
        /// <param name="strNewText">要替换的字符串</param>
        /// <returns>成功返回真，失败返回假</returns>
        public bool SearchReplaceOne(string strOldText, string strNewText)
        {
            object replaceAll = Word.WdReplace.wdReplaceOne;
            object missing = Type.Missing;

            //首先清除任何现有的格式设置选项，然后设置搜索字符串 strOldText。
            this.WordApp.Selection.Find.ClearFormatting();
            WordApp.Selection.Find.Text = strOldText;

            WordApp.Selection.Find.Replacement.ClearFormatting();
            WordApp.Selection.Find.Replacement.Text = strNewText;
            this.WordApp.Selection.Find.Forward = true;
            this.WordApp.Selection.Find.Wrap = WdFindWrap.wdFindContinue;
            if (WordApp.Selection.Find.Execute(
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref replaceAll, ref missing, ref missing, ref missing, ref missing))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 查找替换字符串，替换所有查找到的字符串
        /// </summary>
        /// <param name="strOldText">要查找的字符串</param>
        /// <param name="strNewText">要替换的字符串</param>
        /// <returns>成功返回真，失败返回假</returns>
        public bool SearchReplaceAll(string strOldText, string strNewText)
        {
            object replaceAll = Word.WdReplace.wdReplaceAll;
            object missing = Type.Missing;

            //首先清除任何现有的格式设置选项，然后设置搜索字符串 strOldText。
            this.WordApp.Selection.Find.ClearFormatting();
            WordApp.Selection.Find.Text = strOldText;

            WordApp.Selection.Find.Replacement.ClearFormatting();
            WordApp.Selection.Find.Replacement.Text = strNewText;
            this.WordApp.Selection.Find.Forward = true;
            this.WordApp.Selection.Find.Wrap = WdFindWrap.wdFindContinue;
            if (WordApp.Selection.Find.Execute(
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref replaceAll, ref missing, ref missing, ref missing, ref missing))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 查找字符串
        /// </summary>
        /// <param name="strOldText">要查找的字符串</param>
        /// <returns>存在返回真，不存在返回假</returns>
        public bool SearchReplaceNone(string strOldText)
        {
            object replaceAll = Word.WdReplace.wdReplaceNone;
            object missing = Type.Missing;

            //首先清除任何现有的格式设置选项，然后设置搜索字符串 strOldText。
            this.WordApp.Selection.Find.ClearFormatting();
            WordApp.Selection.Find.Text = strOldText;

            WordApp.Selection.Find.Replacement.ClearFormatting();
            WordApp.Selection.Find.Replacement.Text = "";
            this.WordApp.Selection.Find.Forward = true;
            this.WordApp.Selection.Find.Wrap = WdFindWrap.wdFindContinue;
            if (WordApp.Selection.Find.Execute(
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref replaceAll, ref missing, ref missing, ref missing, ref missing))
            {
                return true;
            }
            return false;
        }
    }

    public enum Word_Alignment
    {
        /// <summary>
        /// 水平左对齐
        /// </summary>
        Algin_Left = Word.WdParagraphAlignment.wdAlignParagraphLeft,

        /// <summary>
        /// 水平居中
        /// </summary>
        Algin_Center = WdParagraphAlignment.wdAlignParagraphCenter,

        /// <summary>
        /// 水平右对齐
        /// </summary>
        Algin_Right = WdParagraphAlignment.wdAlignParagraphRight,

        /// <summary>
        /// 垂直顶部对齐
        /// </summary>
        Algin_Top = WdCellVerticalAlignment.wdCellAlignVerticalTop,

        /// <summary>
        /// 垂直底部对齐
        /// </summary>
        Algin_Bottom = WdCellVerticalAlignment.wdCellAlignVerticalBottom,

        /// <summary>
        /// 垂直居中
        /// </summary>
        Algin_VerticalCenter = WdCellVerticalAlignment.wdCellAlignVerticalCenter,

        /// <summary>
        /// 分散对齐
        /// </summary>
        Algin_Distribute_ = WdParagraphAlignment.wdAlignParagraphDistribute,

        /// <summary>
        /// 两端对齐
        /// </summary>
        Algin_Justify = WdParagraphAlignment.wdAlignParagraphJustify
    }
}