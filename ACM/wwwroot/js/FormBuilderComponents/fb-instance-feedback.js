(function (global, factory) {
    typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
        typeof define === 'function' && define.amd ? define(factory) :
            (global.VueFBInstanceFeedback = factory());
}(this, (function () {
    var index = {
        name: 'fb-instance-feedback',
        props: ['value', 'isfaulureresultchosen', 'readonly'],
        data: function () {
            return {
                isFaulureResultChosen: false
            };
        },
        watch: {
            isfaulureresultchosen: function () {
                this.isFaulureResultChosen = this.isfaulureresultchosen;
            }
        },
        created: function () {
            
        },
        methods: {
            update(key, value) {
                this.$emit('input', _.tap(_.cloneDeep(this.value), v => _.set(v, key, value)));
            },
            updateAll(value) {
                this.$emit('input', value);
            },
            previewFile: function (e) {
                var self = this;
                this.value.supportingImagesBase64 = [];

                for (var i = 0; i < e.target.files.length; i++) {
                    const file = e.target.files[i]

                    if (!file.type.includes('image/')) {
                        alert('Please select an image file');
                        return;
                    }

                    if (typeof FileReader === 'function') {
                        const reader = new FileReader()

                        reader.onload = (event) => {
                            this.value.isPreview = true;
                            this.value.supportingImagesBase64.push(event.target.result);
                            this.updateAll(this.value);
                        }
                        reader.readAsDataURL(file);
                    }
                }
            },
            showImageFullScreen: function (event) {
                var root = document.documentElement;
                var globalreqfullscreen = root.requestFullscreen || root.webkitRequestFullscreen || root.mozRequestFullScreen || root.msRequestFullscreen;

                $(event.target).attr("src", $(event.target).attr("data-imgsrc"));
                globalreqfullscreen.call(event.target);
            },
        },
        template: `<div>
                    <div class="row">
                        <div class="col-md-5">
                            <div class="row">
                                <label class="col-md-3 control-label">Remarks</label>
                                <div class="col-md-9">
                                    <textarea v-model="value.remarks" :disabled="readonly" class="form-control" rows="3" @input="update('remarks', $event.target.value)"></textarea>
                                </div>
                            </div><br />
                        </div>
                        <div class="col-md-7">
                            <div class="row">
                                <label class="col-md-2 control-label">Image Gallery</label>
                                <div class="col-md-10">
                                    <div class="image-container" v-if="value.files != null && value.files.length > 0">
                                        <template v-for="image in value.files">
                                            <a v-bind:href="'/common/RenderFileStoreImg/' + image.fileStoreID" target="_blank"><img v-bind:src="'/common/RenderFileStoreImg/' + image.fileStoreID + '?width=200&height=100'" /></a>
                                        </template>
                                    </div>
                                </div>
                            </div><br />
                            <div class="row" v-if="value.isPreview == true">
                                <label class="col-md-2 control-label">Supporting Images to Upload</label>
                                <div class="col-md-10">
                                    <div class="image-container">
                                        <template v-for="image in value.supportingImagesBase64">
                                            <img v-bind:src="image" class="supportingImageBase64" v-on:click="showImageFullScreen" />
                                        </template>
                                    </div>
                                </div>
                            </div><br />
                            <div class="row" v-if="readonly == false">
                                <label class="col-md-2 control-label">Upload New Images</label>
                                <div class="col-md-10">
                                    <input type="file" multiple class="form-control" v-on:change="previewFile($event)" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>`
    }
    return index;
})));