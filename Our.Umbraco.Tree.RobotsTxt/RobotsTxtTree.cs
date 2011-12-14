namespace Our.Umbraco.Tree.RobotsTxt
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using System.Web;
	using System.Xml;

	using umbraco.cms.presentation.Trees;
	using umbraco.interfaces;

	public class RobotsTxtTree : BaseTree
	{
		public RobotsTxtTree(string applicaton)
			: base(applicaton)
		{
		}

		/// <summary>
		/// Renders the specified tree.
		/// </summary>
		/// <param name="tree">The application tree.</param>
		public override void Render(ref XmlTree tree)
		{
			// nothing happens here (yet)
		}

		/// <summary>
		/// Renders the JS.
		/// </summary>
		/// <param name="javascript">The javascript.</param>
		public override void RenderJS(ref StringBuilder javascript)
		{
			javascript.Append(@"
				function openRobotsTxtEditor() { parent.right.document.location.href = 'robots-txt/editRobotsTxtFile.aspx'; }
			");
		}

		/// <summary>
		/// Creates the root node.
		/// </summary>
		/// <param name="rootNode">The root node.</param>
		protected override void CreateRootNode(ref XmlTreeNode rootNode)
		{
			rootNode.Action = "javascript:openRobotsTxtEditor();";
			rootNode.Text = "Robots.txt";
			rootNode.Icon = "../../robots-txt/robot.png";
			rootNode.OpenIcon = rootNode.Icon;
			rootNode.NodeID = "initRobotsTxt";
			rootNode.NodeType = String.Concat(rootNode.NodeID, this.TreeAlias);
			rootNode.Menu = new List<IAction>();
			rootNode.HasChildren = false;
			rootNode.Source = String.Empty;
		}
	}
}
