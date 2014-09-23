function Music() {
    this.init();
}
(function () {
    var pages = [],
        panels = [],
        selectedItem = null;
    Music.prototype = {
        total:70,
        pageSize:10,
        init:function () {
            var me = this;
            domUtils.on($G("J_searchBtn"), "click", function () {
                me.docopymusic();
            });
            domUtils.on($G("selFile"), "click", function () {
                me.openFile();
                me.docopymusic();
            });
        },
        callback:function (data) {
            var me = this;
            me.data = data.song_list;
            setTimeout(function () {
                $G('J_resultBar').innerHTML = me._renderTemplate(data.song_list);
            }, 300);
        },

        openFile:function(){
            var me = this;
            var data = {};
            //build page
            data.page={};
            data.page.total = 1;
            data.song_list = new Array();

            var files = window.external.MN_OpenMusicFile();
            var item ={};
             item.music_title= files.FileName;
             item.path = files.filePath;
             item.album_title=files.AlbumTitle;
             item.author=files.Author;
             data.song_list.push(item);
            /*
            for(var i=0;i<files.length;i++){
                  var item ={};
                 item.music_title= files[i].FileName;
                 item.path = files[i].filePath;
                 item.album_title=files[i].AlbumTitle;
                 item.author=files[i].Author;
                 data.song_list.push(item);
            }
            */
            me.callback(data);
        },
        docopymusic:function () {
            return;
            var me = this;
            selectedItem = null;
            var selFile = $G('selFile');
            var key = selFile.value;
            if (utils.trim(key) == "")return false;
            var data = {};
            //build page
            data.page={};
            data.page.total = selFile.files.length;
            data.song_list = new Array();
            //call your api :1 copy music files selFile selected.2 fill the real paths of files into the data.song_list.
            var files = selFile.files;
            for(var i=0;i<selFile.files.length;i++){
                  var item ={};
                 item.music_title= files[i].name;
                 item.path = "E:\\Media\\Music\\那谁.mp3";
                 item.album_title="test";
                 item.author="testauthor";
                 data.song_list.push(item);
            }
          
            me.callback(data);
        },
        doselect:function (i) {
            var me = this;
            if (typeof i == 'object') {
                selectedItem = i;
            } else if (typeof i == 'number') {
                selectedItem = me.data[i];
            }
        },
        onpageclick:function (id) {
            var me = this;
            for (var i = 0; i < pages.length; i++) {
                $G(pages[i]).className = 'pageoff';
                $G(panels[i]).className = 'paneloff';
            }
            $G('page' + id).className = 'pageon';
            $G('panel' + id).className = 'panelon';
        },
        listenTest:function (elem) {
            var me = this,
                view = $G('J_preview'),
                is_play_action = (elem.className == 'm-try'),
                old_trying = me._getTryingElem();

            if (old_trying) {
                old_trying.className = 'm-try';
                view.innerHTML = '';
            }
            if (is_play_action) {
                elem.className = 'm-trying';
                var me = this;
                view.innerHTML = me._buildMusicHtml(selectedItem.path);
            }
        },
        _removeHtml:function (str) {
            var reg = /<\s*\/?\s*[^>]*\s*>/gi;
            return str.replace(reg, "");
        },
        _getTryingElem:function () {
            var s = $G('J_listPanel').getElementsByTagName('span');

            for (var i = 0; i < s.length; i++) {
                if (s[i].className == 'm-trying')
                    return s[i];
            }
            return null;
        },
        
        
        
        //<object type="application/x-shockwave-flash" data="dewplayer.swf?mp3=mp3/test1.mp3" width="200" height="20" 
        //id="dewplayer"><param name="wmode" value="transparent" /><param name="movie" value="dewplayer.swf?mp3=mp3/test1.mp3" />
        //</object>
        
        _buildMusicHtml:function (playerUrl) {
            var html = '<embed class="BDE_try_Music" allowfullscreen="false" pluginspage="http://www.macromedia.com/go/getflashplayer"';
            html += ' src="' + playerUrl + '"';
            html += ' width="1" height="1" style="position:absolute;left:-2000px;"';
            html += ' type="application/x-shockwave-flash" wmode="transparent" play="true" loop="false"';
            html += ' menu="false" allowscriptaccess="never" scale="noborder">';
            return html;
        },
        

        


        _byteLength:function (str) {
            return str.replace(/[^\u0000-\u007f]/g, "\u0061\u0061").length;
        },
        _getMaxText:function (s) {
            var me = this;
            s = me._removeHtml(s);
            if (me._byteLength(s) > 12)
                return s.substring(0, 5) + '...';
            if (!s) s = "&nbsp;";
            return s;
        },
        _rebuildData:function (data) {
            var me = this,
                newData = [],
                d = me.pageSize,
                itembox;
            for (var i = 0; i < data.length; i++) {
                if ((i + d) % d == 0) {
                    itembox = [];
                    newData.push(itembox)
                }
                itembox.push(data[i]);
            }
            return newData;
        },
        _renderTemplate:function (data) {
            var me = this;
            if (data.length == 0)return '<div class="empty">' + lang.emptyTxt + '</div>';
            data = me._rebuildData(data);
            var s = [], p = [], t = [];
            s.push('<div id="J_listPanel" class="listPanel">');
            p.push('<div class="page">');
            for (var i = 0, tmpList; tmpList = data[i++];) {
                panels.push('panel' + i);
                pages.push('page' + i);
                if (i == 1) {
                    s.push('<div id="panel' + i + '" class="panelon">');
                    if (data.length != 1) {
                        t.push('<div id="page' + i + '" onclick="music.onpageclick(' + i + ')" class="pageon">' + (i ) + '</div>');
                    }
                } else {
                    s.push('<div id="panel' + i + '" class="paneloff">');
                    t.push('<div id="page' + i + '" onclick="music.onpageclick(' + i + ')" class="pageoff">' + (i ) + '</div>');
                }
                s.push('<div class="m-box">');
                s.push('<div class="m-h"><span class="m-t">' + lang.chapter + '</span><span class="m-s">' + lang.singer
                    + '</span><span class="m-z">' + lang.special + '</span><span class="m-try-t">' + lang.listenTest + '</span></div>');
                for (var j = 0, tmpObj; tmpObj = tmpList[j++];) {
                    s.push('<label for="radio-' + i + '-' + j + '" class="m-m">');
                    s.push('<input type="checkbox" id="radio-' + i + '-' + j + '" name="musicId" class="m-l" onclick="music.doselect(' + (me.pageSize * (i-1) + (j-1)) + ')"/>');
                    s.push('<span class="m-t">' + me._getMaxText(tmpObj.music_title) + '</span>');
                    s.push('<span class="m-s">' + me._getMaxText(tmpObj.author) + '</span>');
                    s.push('<span class="m-z">' + me._getMaxText(tmpObj.album_title) + '</span>');
                    s.push('<span class="m-try" onclick="music.doselect(' + (me.pageSize * (i-1) + (j-1)) + ');music.listenTest(this)"></span>');
                    s.push('</label>');
                }
                s.push('</div>');
                s.push('</div>');
            }
            t.reverse();
            p.push(t.join(''));
            s.push('</div>');
            p.push('</div>');
            return s.join('') + p.join('');
        },
        exec:function () {
            var me = this;
            if (selectedItem == null)   return;
            $G('J_preview').innerHTML = "";
            var html = '<object type="application/x-shockwave-flash" data="dialogs\\music\\dewplayer.swf?mp3=';
            html += ( selectedItem.path + '" width="200" height="20" ');
            html += ' id="dewplayer"><param name="wmode" value="transparent" /><param name="movie" value="dialogs\\music\\dewplayer.swf?mp3=';
            html += ( selectedItem.path +'" />');
            html += ' </object>';
            /*editor.execCommand('music', {
                url:selectedItem.path,
                width:400,
                height:95
            });
            */
            editor.execCommand('insertHtml',html);
        },
        _buildMusicPreviewHtml:function (playerUrl) {
            var html = '<object type="application/x-shockwave-flash" data="dialogs\\music\\dewplayer.swf?mp3=';
            html += ( playerUrl + '" width="200" height="20" ');
            html += ' id="dewplayer"><param name="wmode" value="transparent" /><param name="movie" value="dialogs\\music\\dewplayer.swf?mp3=';
            html += ( playerUrl +'" />');
            html += ' </object>';
            return html;
        },
    };
})();



