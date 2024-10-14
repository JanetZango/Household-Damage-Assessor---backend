(function (global, factory) {
    typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
        typeof define === 'function' && define.amd ? define(factory) :
            (global.VueFBInstanceSignature = factory());
}(this, (function () {
    var index = {
        name: 'fb-instance-signature',
        props: ['value', 'iindex', 'cindex', 'sindex', 'iiindex', 'categoryitems', 'readonly', 'diindex'],
        data: function () {
            return {
                
            };
        },
        watch: {
            signatureBase64: function () {
                this.validate();
            }
        },
        computed: {
            signatureBase64: function () {
                return this.value.instanceProperties.signatureBase64;
            },
            isMandatory: function () {
                var returnValue = false;
                var self = this;

                if (self.value.definitionProperties.isMandatory == true) {
                    returnValue = true;
                }
                else if (self.value != undefined && self.value.definitionProperties != undefined && self.value != null && self.value.definitionProperties != null
                    && self.categoryitems != undefined
                    && self.value.definitionProperties.isCondition == true && self.value.definitionProperties.conditionActionEventCode == 'REQUIRED') {

                    var parentQuestion = $.grep(self.categoryitems, function (item) {
                        var returnValue = false;

                        if (item.childCode == 'QUESTION') {
                            if (item.definitionChildID.toLowerCase() == self.value.definitionProperties.conditionFormDefinitionItemQuestionID.toLowerCase()) {
                                return true;
                            }
                        }

                        return returnValue;
                    });

                    if (parentQuestion != undefined && parentQuestion != null && parentQuestion.length > 0) {
                        var selectedAnswer = $.grep(parentQuestion[0].instanceProperties.formItemQuestionResponseID, function (answer) {
                            return answer.toLowerCase() == self.value.definitionProperties.conditionFormDefinitionItemQuestionResponseID.toLowerCase();
                        });

                        if (selectedAnswer != null && selectedAnswer.length > 0) {
                            returnValue = true;
                        }
                    }
                }

                return returnValue;
            },
            visible: function () {
                var returnValue = false;
                var self = this;

                if (self.value != undefined && self.value.definitionProperties != undefined && self.value != null && self.value.definitionProperties != null
                    && self.categoryitems != undefined && self.value.definitionProperties.isCondition == true) {

                    var selectedAnswer;
                    var parentQuestion = $.grep(self.categoryitems, function (item) {
                        var returnValue = false;

                        if (item.childCode == 'QUESTION') {
                            if (item.definitionChildID.toLowerCase() == self.value.definitionProperties.conditionFormDefinitionItemQuestionID.toLowerCase()) {
                                return true;
                            }
                        }

                        return returnValue;
                    });

                    if (self.value.definitionProperties.conditionActionEventCode == 'VISIBLE') {

                        if (parentQuestion != undefined && parentQuestion != null && parentQuestion.length > 0) {
                            selectedAnswer = $.grep(parentQuestion[0].instanceProperties.formItemQuestionResponseID, function (answer) {
                                return answer.toLowerCase() == self.value.definitionProperties.conditionFormDefinitionItemQuestionResponseID.toLowerCase();
                            });

                            if (selectedAnswer != null && selectedAnswer.length > 0) {
                                returnValue = true;
                            }
                            else {
                                returnValue = false;
                            }
                        }
                    }
                    else if (self.value.definitionProperties.conditionActionEventCode == 'HIDDEN') {

                        if (parentQuestion != undefined && parentQuestion != null && parentQuestion.length > 0) {
                            selectedAnswer = $.grep(parentQuestion[0].instanceProperties.formItemQuestionResponseID, function (answer) {
                                return answer.toLowerCase() == self.value.definitionProperties.conditionFormDefinitionItemQuestionResponseID.toLowerCase();
                            });

                            if (selectedAnswer != null && selectedAnswer.length > 0) {
                                returnValue = false;
                            }
                            else {
                                returnValue = true;
                            }
                        }
                    }
                    else {
                        returnValue = true;
                    }
                }
                else {
                    returnValue = true;
                }

                return returnValue;
            }
        },
        created: function () {
            
        },
        mounted: function () {
            var self = this;
            setTimeout(function () { self.validate(); }, 1000);
        },
        methods: {
            update(key, value) {
                this.$emit('input', _.tap(_.cloneDeep(this.value), v => _.set(v, key, value)));
            },
            updateAll(value) {
                this.$emit('input', value);
            },
            isFaulureResultChosen: function () {
                var returnValue = false;

                return returnValue;
            },
            validate: function () {
                if (this.isMandatory === true && this.visible == true) {
                    if (this.signatureBase64 != null && this.signatureBase64 != '') {
                        this.value.isValid = true;
                    }
                    else {
                        this.value.isValid = false;
                    }
                }
                else {
                    this.value.isValid = true;
                }
            },
            openSingnaturePad: function (event) {
                var canvasObj = $(event.target).parents(".signatureContainer").find("canvas");
                var imgObj = $(event.target).parents(".signatureContainer").find("img");
                var editBtn = $(event.target).parents(".signatureContainer").find(".NewSignature");
                var saveBtn = $(event.target).parents(".signatureContainer").find(".SaveSignature");

                imgObj.hide();
                canvasObj.show();
                editBtn.hide();
                saveBtn.show();

                this.value.instanceProperties.SignaturePad = new SignaturePad(canvasObj[0]);

                var ratio = Math.max(window.devicePixelRatio || 1, 1);
                canvasObj[0].width = canvasObj[0].offsetWidth * ratio;
                canvasObj[0].height = canvasObj[0].offsetHeight * ratio;
                canvasObj[0].getContext("2d").scale(ratio, ratio);

                this.value.instanceProperties.SignaturePad.clear();
            },
            saveSingnaturePad: function (event) {
                var canvasObj = $(event.target).parents(".signatureContainer").find("canvas");
                var imgObj = $(event.target).parents(".signatureContainer").find("img");
                var editBtn = $(event.target).parents(".signatureContainer").find(".NewSignature");
                var saveBtn = $(event.target).parents(".signatureContainer").find(".SaveSignature");

                imgObj.show();
                canvasObj.hide();
                editBtn.show();
                saveBtn.hide();

                var data = this.value.instanceProperties.SignaturePad.toDataURL();
                this.value.instanceProperties.isNewSignature = true;
                this.value.instanceProperties.signatureBase64 = data;

                this.value.instanceProperties.SignaturePad.clear();
                this.value.instanceProperties.SignaturePad = null;

                this.updateAll(this.value);
            },
        },
        template: `<div class="number fb-instance-item" v-if="value.childCode == 'SIGNATURE' && visible == true">
                        <div class="row">
                              <div class="col-md-12">
                                   <strong class="item-description">{{value.childDescription}}</strong> <span style="color:red;" v-if="isMandatory == true">*</span>
                              </div>
                        </div>
                        <div class="row">
                              <div class="col-md-12">
                                    <ul class="nav nav-tabs" role="tablist">
                                        <li class="nav-item">
                                            <a class="nav-link active" v-bind:href="'#fbitemresult' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex" v-bind:id="'fbitemresult-tab' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex" role="tab" data-toggle="tab" v-bind:aria-controls="'fbitemresult' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex" aria-expanded="true">Result</a></li>
                                        <li class="nav-item">
                                            <a class="nav-link" v-bind:href="'#fbitemfeedback' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex" role="tab" v-bind:id="'fbitemfeedback-tab' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex" data-toggle="tab" v-bind:aria-controls="'fbitemfeedback' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex"><i v-if="(value.instanceItem.remarks != null && value.instanceItem.remarks.length > 0) || (value.instanceItem.files != null && value.instanceItem.files.length > 0)" class="anglo-blue fa fa-check"></i> Feedback</a>
                                        </li>
                                    </ul>
                                    <div class="tab-content px-1 pt-1 border-left border-bottom border-right">
                                        <div role="tabpanel" class="tab-pane active p-1" v-bind:id="'fbitemresult' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex" v-bind:aria-labelledby="'fbitemresult-tab' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex">
                                            <div class="row">
                                                <div class="col-md-6" v-if="readonly == false">
                                                    <div class="row signatureContainer pad-btm">
                                                        <div class="col-md-2">
                                                            <button type="button" v-on:click="openSingnaturePad($event)" class="btn btn-info btn-rounded NewSignature" title="New Signature"><i class="fa fa-pencil"></i> New</button>
                                                            <button type="button" v-on:click="saveSingnaturePad($event)" class="btn btn-primary btn-rounded SaveSignature" style="display:none" title="Save Signature"><i class="fa fa-save"></i> Save</button>
                                                        </div>
                                                        <div class="col-md-9">
                                                            <div class="signature-container">
                                                                <img v-if="value.instanceProperties.isNewSignature == true" class="img-responsive" v-bind:src="value.instanceProperties.signatureBase64" />
                                                                <img v-if="value.instanceProperties.isNewSignature == false && value.instanceProperties.signatureBase64 != null && value.instanceProperties.signatureBase64 != ''" class="img-responsive" v-bind:src="value.instanceProperties.signatureBase64" />
                                                                <canvas style="height:200px; width:100%; display:none;"></canvas>
                                                                <div class="signature-pad" style="text-align: center;" v-if="value.instanceProperties.signatureBase64 == '' || value.instanceProperties.signatureBase64 == null">
                                                                    <img src="/img/signature-here.png" style="height: 240px;" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-2"></div>
                                                        <div class="col-md-9">Note: Please remember to save your signature after signing</div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6" v-if="readonly == true">
                                                    <img style="max-height:200px;" v-if="value.instanceProperties.signatureBase64 != null && value.instanceProperties.signatureBase64 != ''" v-bind:src="value.instanceProperties.signatureBase64" />
                                                </div>
                                                <div class="col-md-6">
                                                    <fb-instance-caution-instruction v-model="value"></fb-instance-caution-instruction>
                                                </div>
                                            </div>
                                        </div>
                                        <div role="tabpanel" class="tab-pane" v-bind:id="'fbitemfeedback' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex" v-bind:aria-labelledby="'fbitemfeedback-tab' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex">
                                              <fb-instance-feedback v-model="value.instanceItem" :readonly="readonly" :isfaulureresultchosen="isFaulureResultChosen()"></fb-instance-feedback>
                                        </div>
                                    </div>
                              </div>
                        </div>
                    </div>`
    }
    return index;
})));