// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;

namespace Markdig.Wpf
{
    /// <summary>
    /// A markdown viewer control.
    /// </summary>
    public class MarkdownViewer : Control
    {
        protected static readonly MarkdownPipeline DefaultPipeline = new MarkdownPipelineBuilder().UseSupportedExtensions().Build();

        private static readonly DependencyPropertyKey DocumentPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(Document), typeof(FlowDocument), typeof(MarkdownViewer), new FrameworkPropertyMetadata());

        /// <summary>
        /// Defines the <see cref="Document"/> property.
        /// </summary>
        public static readonly DependencyProperty DocumentProperty = DocumentPropertyKey.DependencyProperty;

        /// <summary>
        /// Defines the <see cref="Markdown"/> property.
        /// </summary>
        public static readonly DependencyProperty MarkdownProperty =
       DependencyProperty.Register(nameof(Markdown), typeof(string), typeof(MarkdownViewer), new FrameworkPropertyMetadata(MarkdownChanged));

        /// <summary>
        /// Defines the <see cref="Pipeline"/> property.
        /// </summary>
        public static readonly DependencyProperty PipelineProperty =
            DependencyProperty.Register(nameof(Pipeline), typeof(MarkdownPipeline), typeof(MarkdownViewer), new FrameworkPropertyMetadata(PipelineChanged));

        /// <summary>
        /// Defines the <see cref="UpdateDelay"/> property - delay in milliseconds before updating the document.
        /// </summary>
        public static readonly DependencyProperty UpdateDelayProperty =
            DependencyProperty.Register(nameof(UpdateDelay), typeof(int), typeof(MarkdownViewer), new FrameworkPropertyMetadata(0));

        private DispatcherTimer? _updateTimer;
        private bool _hasPendingUpdate;

        static MarkdownViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MarkdownViewer), new FrameworkPropertyMetadata(typeof(MarkdownViewer)));
        }

        /// <summary>
        /// Gets the flow document to display.
        /// </summary>
        public FlowDocument? Document
        {
            get { return (FlowDocument)GetValue(DocumentProperty); }
            protected set { SetValue(DocumentPropertyKey, value); }
        }

        /// <summary>
        /// Gets or sets the markdown to display.
        /// </summary>
        public string? Markdown
        {
            get { return (string)GetValue(MarkdownProperty); }
            set { SetValue(MarkdownProperty, value); }
        }

        /// <summary>
        /// Gets or sets the markdown pipeline to use.
        /// </summary>
        public MarkdownPipeline Pipeline
        {
            get { return (MarkdownPipeline)GetValue(PipelineProperty); }
            set { SetValue(PipelineProperty, value); }
        }

        /// <summary>
        /// Gets or sets the update delay in milliseconds. 
        /// Set to 0 for immediate updates, or a positive value (e.g., 100-300) to throttle rapid updates.
        /// This is useful for streaming scenarios to prevent excessive re-parsing.
        /// </summary>
        public int UpdateDelay
        {
            get { return (int)GetValue(UpdateDelayProperty); }
            set { SetValue(UpdateDelayProperty, value); }
        }

        private static void MarkdownChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (MarkdownViewer)sender;
            control.ScheduleRefreshDocument();
        }

        private static void PipelineChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (MarkdownViewer)sender;
            control.RefreshDocument();
        }

        private void ScheduleRefreshDocument()
        {
            var delay = UpdateDelay;

            if (delay <= 0)
            {
                // Immediate update
                RefreshDocument();
                return;
            }

            // Throttle: if timer is already running, just mark that we have a pending update
            if (_updateTimer != null && _updateTimer.IsEnabled)
            {
                _hasPendingUpdate = true;
                return;
            }

            // Start a new timer
            _hasPendingUpdate = false;

            if (_updateTimer == null)
            {
                _updateTimer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromMilliseconds(delay)
                };
                _updateTimer.Tick += OnUpdateTimerTick;
            }
            else
            {
                _updateTimer.Interval = TimeSpan.FromMilliseconds(delay);
            }

            RefreshDocument(); // Update immediately
            _updateTimer.Start();
        }

        private void OnUpdateTimerTick(object? sender, EventArgs e)
        {
            _updateTimer?.Stop();

            // If there were updates while timer was running, refresh one more time
            if (_hasPendingUpdate)
            {
                _hasPendingUpdate = false;
                RefreshDocument();
            }
        }

        protected virtual void RefreshDocument()
        {
            Document = Markdown != null ? Wpf.Markdown.ToFlowDocument(Markdown, Pipeline ?? DefaultPipeline) : null;
        }
    }
}
