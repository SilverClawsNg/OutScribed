(function () {
	window.QuillFunctions = {
		createQuill: function (
			quillElement, toolBar, readOnly,
			placeholder, theme, debugLevel) {

			var options = {
				debug: debugLevel,
				modules: {
					toolbar: toolBar
				},
				placeholder: placeholder,
				readOnly: readOnly,
				theme: theme
			};

			new Quill(quillElement, options);
		},
		getQuillContent: function (quillElement) {
			return JSON.stringify(quillElement.__quill.getContents());
		},
		getQuillText: function (quillElement) {
			return quillElement.__quill.getText();
		},
		getQuillHTML: function (quillElement) {
			return quillElement.__quill.root.innerHTML;
		},
		loadQuillContent: function (quillElement, quillContent) {
			content = JSON.parse(quillContent);
			return quillElement.__quill.setContents(content, 'api');
		},
		enableQuillEditor: function (quillElement, mode) {
			quillElement.__quill.enable(mode);
		},
		insertQuillImage: function (quillElement, imageURL) {
			var Delta = Quill.import('delta');
			editorIndex = 0;

			if (quillElement.__quill.getSelection() !== null) {
				editorIndex = quillElement.__quill.getSelection().index;
			}

			return quillElement.__quill.updateContents(
				new Delta()
					.retain(editorIndex)
					.insert({ image: imageURL },
						{ alt: imageURL }));
		}
	};
})();








//(function () {
//    window.QuillFunction = {
//        createQuill: function (quillElement) {
//            var options = {
//                debug: 'info',
//                modules: {
//                    toolbar: '#toolbar'
//                },
//                placeholder: 'Type here...',
//                readOnly: false,
//                theme: 'snow'
//            };
//            // set quill at the object we can call
//            // methods on later
//            new Quill(quillElement, options);
//        },
//        getQuillHTML: function (quillControl) {
//            return quillControl.__quill.root.innerHTML;
//        }
//    };
//})();

