(function (global, factory) {
    typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
        typeof define === 'function' && define.amd ? define(factory) :
            (global.VueFBDefinitionPicture = factory());
}(this, (function () {
    var index = {
        name: 'fb-definition-picture',
        props: ['value', 'iindex', 'cindex', 'sindex', 'iiindex', 'readonly'],
        data: function () {
            return {
                
            };
        },
        computed: {

        },
        watch: {

        },
        created: function () {

        },
        methods: {
            update(key, value) {
                this.$emit('input', _.tap(_.cloneDeep(this.value), v => _.set(v, key, value)));
            },
            subSelectResponseItem(event) {
                this.$emit('select-item', this.value, event, this.iindex, this.cindex, this.sindex, this.iiindex);
            },
            RemovePicture: function (pindex) {
                this.$emit('input', _.tap(_.cloneDeep(this.value), v => v.properties.pictures.splice(pindex, 1)))
            },
            BrowsePicture: function () {
                $("#file_upload_picture_" + this.value.formDefinitionItemID).click();
            },
            LoadPicturePreview: function (event) {
                var self = this;

                const file = event.target.files[0];

                if (!file.type.includes('image/')) {
                    bootbox.alert("Please select an image file (.png, .jpg)");
                    return;
                }
                if ((file.size / 1024) > 4000) {
                    bootbox.alert("Please insure that your image file is smaller than 4MB?");
                    return;
                }

                if (typeof FileReader === 'function') {
                    const reader = new FileReader()

                    reader.onload = function (loadevent) {
                        if (self.value.properties.pictures == undefined || self.value.properties.pictures == null) {
                            this.$emit('input', _.tap(_.cloneDeep(this.value), v => _.set(v, "properties.pictures", [])));
                        }
                        self.$emit('input', _.tap(_.cloneDeep(self.value), v => v.properties.pictures.push(loadevent.target.result)))
                    }
                    reader.readAsDataURL(file);
                }
            },
        },
        template: `<div class="picture" v-if="value.childCode == 'PICTURE'" v-on:dblclick="subSelectResponseItem($event)">
                        <div class="row">
                            <div class="col-md-11 question-heading">
                                {{value.childDescription}} <span v-if="value.properties.isMandatory == true">*</span>
                            </div>
                            <div class="col-md-1 text-center" v-if="readonly == false">
                                <i class="fa fa-pen edititem" :id="'edititem_' + value.formDefinitionItemID" v-on:click="subSelectResponseItem($event)"></i>
                            </div>
                        </div>
                        <div class="row instructions" v-if="value.properties.isInstruction == true">
                            <div class="col-md-12 pad-btm">
                                <span class="instruction" role="button" data-toggle="collapse" :href="'#question_instruction_' + value.formDefinitionItemID" aria-expanded="true"><i class="fa fa-eye"></i> Toggle instructions</span>
                                <div class="collapse in" :id="'question_instruction_' + value.formDefinitionItemID">
                                    <div v-html="value.properties.instructionText"></div>
                                    <img :src="value.properties.instructionImageBase64" v-if="value.properties.instructionImageBase64 != ''" class="img-responsive" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="fb-picture-container">
                                    <div class="fb-picture-item" v-for="(picture, pindex) in value.properties.pictures">
                                        <img v-if="picture != ''" :src="picture" />
                                        <div class="remove-image" v-on:click="RemovePicture(pindex)">
                                            <i class="fa fa-times"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" v-if="(value.properties.pictures == null || value.properties.pictures.length == 0) || (value.properties.allowMultipleUploads == true)">
                            <div class="col-md-12" style="padding-left: 15px;">
                                <div class="fb-upload-image" v-on:click="BrowsePicture()">
                                    <i class="fa fa-plus"></i><br />
                                    <span>Upload image</span><br />
                                    <input type="file" style="display:none" :id="'file_upload_picture_' + value.formDefinitionItemID" v-on:change="LoadPicturePreview($event)" />
                                </div>
                            </div>
                        </div>
                    </div>`
    };
    return index;
})));